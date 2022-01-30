import React from "react";
import { Callback, ElementStorage, StorageTarget } from "../Helpers/ElementStorage";
import ILog from "../Models/ILog";
import LogRow from "./LogRow";
import {ArrowLeft, ArrowRight} from "react-bootstrap-icons"

interface IProps { }

interface IState {
	isLoading: boolean;
	logs: Array<ILog>;
	currentPage: number;
	numberOfPages: number;
	pageSize: number;
}

class LogTable extends React.Component<IProps, IState> {

	callbacks: Array<Callback>;
	readonly storage: ElementStorage;

	constructor(props: IProps) {
		super(props);
		this.state = {
			isLoading: true,
			logs: [],
			currentPage: 1,
			numberOfPages: 1,
			pageSize: 10,
		};
		this.storage = ElementStorage.getInstance();
		this.callbacks = [
			new Callback((target: StorageTarget) => {
				const data = 
					this.storage.getData<Array<ILog>>(target);
				if(!data.isError && data.data != undefined){
					this.setState({
						isLoading: false,
						logs: data.data,
						numberOfPages:
							Math.floor(data.data.length / this.state.pageSize) +
							(data.data.length % this.state.pageSize > 0 ? 1 : 0),
						currentPage: 
							Math.floor(data.data.length / this.state.pageSize) +
							(data.data.length % this.state.pageSize > 0 ? 1 : 0) == 0 ? 0 : 1
					});
				}
			}, StorageTarget.LOGS)
		]
	}

	componentDidMount(){
		this.storage.updateParams({
			sessionId: sessionStorage.getItem("sessionId")
		}, StorageTarget.LOGS);
		this.callbacks.forEach(callback => this.storage.subscribe(callback));
	}

	componentWillUnmount(){
		this.callbacks.forEach(callback => this.storage.unsubscribe(callback));
	}

	render() {
		return (
			<div>
				<table className="table table-hover table-bordered">
					<thead>
						<tr>
							<th>Poziom</th>
							<th>Data</th>
							<th>Osoba</th>
							<th>Akcja</th>
						</tr>
					</thead>
					<tbody className="align-middle">
						{!this.state.isLoading ? (
							this.state.logs
								.slice(
									(this.state.currentPage - 1) * this.state.pageSize,
									Math.min(
										this.state.logs.length,
										(this.state.currentPage - 1) * this.state.pageSize +
										this.state.pageSize
									)
								)
								.map((val) => {
									return <LogRow key={val.id} item={val} />;
								})
						) : (
							<tr>
								<td style={{ textAlign: "center" }} colSpan={5}>
									Ładowanie...
								</td>
							</tr>
						)}
					</tbody>
				</table>
				<div className="d-flex justify-content-center">
					<div>
					<button
						className="button btn-primary me-3"
						disabled={this.state.currentPage <= 1}
						onClick={() =>
							this.setState({ currentPage: this.state.currentPage - 1 })
						}
					><ArrowLeft/>
					</button>
						<span className="align-middle">Strona {this.state.currentPage} z {this.state.numberOfPages}</span>
					<button
						className="button btn-primary ms-3 text-center align-middle"
						disabled={this.state.currentPage >= this.state.numberOfPages}
						onClick={() =>
							this.setState({ currentPage: this.state.currentPage + 1 })
						}
					><ArrowRight/>
					</button>
					<span className="ms-3 align-middle">Ilość logów w tabeli: </span>
					<select className="align-middle"
						onChange={(event: React.ChangeEvent<HTMLSelectElement>) => {
							this.setState({ pageSize: +event.target.value });
							console.log(event);
						}}
						value={this.state.pageSize}
					>
						<option value="5">5</option>
						<option value="10">10</option>
						<option value="15">15</option>
						<option value="20">20</option>
					</select>
					</div>
				</div>
			</div>
		);
	}
}

export default LogTable;
