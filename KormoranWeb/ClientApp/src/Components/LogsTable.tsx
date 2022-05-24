import React from "react";
import ILog from "../Models/ILog";
import LogRow from "./LogRow";
import { ArrowLeft, ArrowRight } from "react-bootstrap-icons";
import { Empty } from "../Helpers/Aliases";
/*import { DownloadManager, DEFAULT_TIMEOUT } from "../Helpers/DownloadManager";
import { GET_LOGS } from "../Helpers/Endpoints";
import { ICollectionResponse } from "../Models/IResponses";
import { ILogsParams } from "../Models/IRequests";*/

interface IState {
    isLoading: boolean;
    logs: Array<ILog>;
    currentPage: number;
    numberOfPages: number;
    pageSize: number;
}

class LogTable extends React.Component<Empty, IState> {
	//readonly logsDownloader: DownloadManager<ICollectionResponse<ILog>, ILogsParams>;
	constructor(props: Empty) {
		super(props);
		this.state = {
			isLoading: true,
			logs: [],
			currentPage: 1,
			numberOfPages: 1,
			pageSize: 10,
		};
		/*this.logsDownloader = new DownloadManager<ICollectionResponse<ILog>, ILogsParams>(
            GET_LOGS, DEFAULT_TIMEOUT, (data: unknown): void => {
                console.log(data);
                /*this.setState({
                    isLoading: false,
                    logs: data.collection,
                    numberOfPages:
                        Math.floor(data.collection.length / this.state.pageSize) +
                        (data.collection.length % this.state.pageSize > 0 ? 1 : 0),
                    currentPage:
                        Math.floor(data.collection.length / this.state.pageSize) +
                            (data.collection.length % this.state.pageSize > 0 ? 1 : 0) == 0 ? 0 : 1
                });
            }
        ).setParams({
            sessionId: sessionStorage.getItem("sessionId") ?? ""
        });*/
	}

	componentDidMount(): void {
		//this.logsDownloader.start();
	}

	componentWillUnmount(): void {
		//this.logsDownloader.destroy();
	}

	render(): JSX.Element {
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
							onClick={(): void =>
								this.setState({ currentPage: this.state.currentPage - 1 })
							}
						><ArrowLeft />
						</button>
						<span className="align-middle">Strona {this.state.currentPage} z {this.state.numberOfPages}</span>
						<button
							className="button btn-primary ms-3 text-center align-middle"
							disabled={this.state.currentPage >= this.state.numberOfPages}
							onClick={(): void =>
								this.setState({ currentPage: this.state.currentPage + 1 })
							}
						><ArrowRight />
						</button>
						<span className="ms-3 align-middle">Ilość logów w tabeli: </span>
						<select className="align-middle"
							onChange={(e): void => {
								this.setState({ pageSize: +e.target.value });
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