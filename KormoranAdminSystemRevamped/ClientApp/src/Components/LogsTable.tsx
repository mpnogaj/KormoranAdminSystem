import React from "react";
import ILog from "../Models/ILog";
import LogRow from "./LogRow";

interface IProps{
	pageSize: number
}

interface IState{
	isLoading: boolean,
	logs: Array<ILog>,
	currentPage: number,
	numberOfPages: number
}

class LogTable extends React.Component<IProps, IState>{

	constructor(props: IProps) {
		super(props);
		this.state = {
			isLoading: true,
			logs: [],
			currentPage: 1,
			numberOfPages: 1
		}
		this.LoadLogs = this.LoadLogs.bind(this);
		this.LoadLogs().catch((ex) => console.log(ex));
	}

	async LoadLogs(){
		const data: Array<ILog> = [{
			id: 1,
			level: 1,
			date: new Date(),
			author: "Marcin Nogaj",
			action: "Zalogowano"
		}, {
			id: 2,
			level: 1,
			date: new Date(),
			author: "Szymon W",
			action: "Stworzył nowy turniej"
		}];

		this.setState({
			isLoading: false,
			logs: data,
			numberOfPages: data.length / this.props.pageSize + data.length % this.props.pageSize > 0 ? 1 : 0
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
					{
						this.state.isLoading
							?
							this.state.logs.map((val) => {
								return (
									<LogRow/>
								);
							})
							:
							<tr>
								<td style={{textAlign: "center"}} colSpan={5}>Ładowanie...</td>
							</tr>
					}
					</tbody>
				</table>
			</div>
		)
	}
}

export default LogTable;