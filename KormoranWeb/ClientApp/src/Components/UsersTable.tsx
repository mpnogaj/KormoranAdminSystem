import axios from "axios";
import React from "react";
import { Badge, Table } from "react-bootstrap";
import { Empty } from "../Helpers/Aliases";
import { DEFAULT_TIMEOUT, DownloadManager } from "../Helpers/DownloadManager";
import { GET_USERS } from "../Helpers/Endpoints";
import { IAdminCheckResponse, ICollectionResponse } from "../Models/IResponses";
import IUser from "../Models/IUser";

interface IState {
	isAdmin: boolean,
	isLoading: boolean,
	users: Array<IUser>;
}

class UsersTable extends React.Component<Empty, IState> {

	readonly userDownloader: DownloadManager<ICollectionResponse<IUser>, null>;

	constructor(props: Empty) {
		super(props);
		this.userDownloader = new DownloadManager<ICollectionResponse<IUser>, null>(
			GET_USERS, DEFAULT_TIMEOUT, (response) => {
				console.log(response.collection);
				this.setState({users: response.collection, isLoading: false});
			});
		this.state = {
			isLoading: false,
			isAdmin: false,
			users: []
		};
	}

	componentDidMount(): void{
		axios.get<Empty, IAdminCheckResponse>("/api/User/IsAdmin")
			.then(x => this.setState({isAdmin: x.isAdmin}))
			.catch(_ => this.setState({isAdmin: false}));
		this.userDownloader.start();
	}

	componentWillUnmount(): void {
		this.userDownloader.destroy();
	}

	render(): JSX.Element {
		return (
			<div>
				<Table responsive hover bordered>
					<thead>
						<tr>
							<th>Id</th>
							<th>Nazwa użytkownika</th>
							<th>Imię i nazwisko</th>
							<th>Typ konta</th>
							{
								this.state.isAdmin ? <th>Akcja</th> : null
							}
						</tr>
					</thead>
					<tbody>
						{
							this.state.isLoading
								?
								<tr>Ładowanie</tr>
								:
								this.state.users.map(u => {
									return(
										<tr key={u.id}>
											<td>{u.id}</td>
											<td>{u.login}</td>
											<td>{u.fullname}</td>
											<td>{
												u.isAdmin 
													? <Badge bg="warning">Administrator</Badge> 
													: <Badge bg="success">Sędzia</Badge>
											}</td>
										</tr>
									);
								})
						}
					</tbody>
				</Table>
			</div>
		);
	}
}

export default UsersTable;