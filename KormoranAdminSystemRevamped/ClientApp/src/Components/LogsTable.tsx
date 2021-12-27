import axios from "axios";
import React, { ReactEventHandler } from "react";
import ILog from "../Models/ILog";
import ILogsResponse from "../Models/Responses/ILogsResponse";
import LogRow from "./LogRow";

interface IProps { }

interface IState {
	isLoading: boolean;
	logs: Array<ILog>;
	currentPage: number;
	numberOfPages: number;
	pageSize: number;
}

class LogTable extends React.Component<IProps, IState> {
	constructor(props: IProps) {
		super(props);
		this.state = {
			isLoading: true,
			logs: [],
			currentPage: 1,
			numberOfPages: 1,
			pageSize: 10,
		};
		this.LoadLogs = this.LoadLogs.bind(this);
		this.LoadLogs().catch((ex) => console.log(ex));
	}

	async LoadLogs() {
		const response = await axios.get<ILogsResponse>("/api/Logs/GetLogs", {
			params: {
				sessionId: sessionStorage.getItem("sessionId"),
			},
		});
		const data = response.data.logEntries;
		this.setState({
			isLoading: false,
			logs: data,
			numberOfPages:
				Math.floor(data.length / this.state.pageSize) +
				(data.length % this.state.pageSize > 0 ? 1 : 0),
		});
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
						{this.state.isLoading == false ? (
							this.state.logs
								.slice(
									this.state.currentPage - 1 * this.state.pageSize,
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
				<div className="align-center">
					<button
						className="button btn-primary me-3"
						disabled={this.state.currentPage <= 1}
						onClick={() =>
							this.setState({ currentPage: this.state.currentPage - 1 })
						}
					>&#8592;
					</button>
					Strona {this.state.currentPage} z {this.state.numberOfPages}
					<button
						className="button btn-primary ms-3"
						disabled={this.state.currentPage >= this.state.numberOfPages}
						onClick={() =>
							this.setState({ currentPage: this.state.currentPage + 1 })
						}
					>&#8594;
					</button>
					<span className="ms-3">Ilość logów w tabeli: </span>
					<select
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
		);
	}
}

export default LogTable;
