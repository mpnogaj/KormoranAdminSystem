import axios, { AxiosResponse } from "axios";
import React from "react";
import { Badge, Button, Modal, Table } from "react-bootstrap";
import { Empty } from "../Helpers/Aliases";
import { DEFAULT_TIMEOUT, DownloadManager } from "../Helpers/DownloadManager";
import { GET_USERS } from "../Helpers/Endpoints";
import { binsearch } from "../Helpers/Essentials";
import { IAdminCheckResponse, ICollectionResponse } from "../Models/IResponses";
import IconButton from "../Components/IconButton";
import { Pencil, Trash } from "react-bootstrap-icons";
import IUser, { DEFAULT_USER } from "../Models/IUser";

interface IState {
	isAdmin: boolean,
	isLoading: boolean,
	user: IUser,
	addEditModalShowed: boolean,
	users: Array<IUser>;
}

class UsersTable extends React.Component<Empty, IState> {

	readonly userDownloader: DownloadManager<ICollectionResponse<IUser>, null>;

	constructor(props: Empty) {
		super(props);
		this.userDownloader = new DownloadManager<ICollectionResponse<IUser>, null>(
			GET_USERS, DEFAULT_TIMEOUT, (response) => {
				console.log(response.collection);
				this.setState({ users: response.collection, isLoading: false });
			});
		this.state = {
			isLoading: false,
			isAdmin: false,
			user: DEFAULT_USER,
			addEditModalShowed: false,
			users: []
		};
	}

	componentDidMount(): void {
		axios.get<Empty, AxiosResponse<IAdminCheckResponse>>("/api/User/IsAdmin")
			.then(x => {
				console.log(x);
				this.setState({ isAdmin: x.data.isAdmin });
			})
			.catch(_ => this.setState({ isAdmin: false }));
		this.userDownloader.start();
	}

	componentWillUnmount(): void {
		this.userDownloader.destroy();
	}

	showAddEditModal = (userId: number): void => {
		const bsRes: IUser | undefined = binsearch<IUser>(this.state.users, (x) => x.id - userId);
		const user: IUser = bsRes == undefined ? DEFAULT_USER : bsRes;
		this.setState({addEditModalShowed: true, user: user});
	};

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
									return (
										<tr key={u.id}>
											<td>{u.id}</td>
											<td>{u.login}</td>
											<td>{u.fullname}</td>
											<td>{
												u.isAdmin
													? <Badge bg="warning">Administrator</Badge>
													: <Badge bg="success">Sędzia</Badge>
											}</td>
											{
												this.state.isAdmin
													?
													<td>
														<div>
															<IconButton icon={<Pencil height={24} width={24} />} onClick={async (): Promise<void> => {
																console.log("tba");
															}}/>
															<IconButton icon={<Trash height={24} width={24} />} onClick={async (): Promise<void> => {
																console.log("tba");
															}} />
														</div>
													</td>
													:
													null
											}
										</tr>
									);
								})
						}
					</tbody>
				</Table>
				{
					this.state.isAdmin ? <Button onClick={(): void => this.showAddEditModal(0)}>Dodaj konto</Button> : null
				}
				<Modal show={this.state.addEditModalShowed}
					onHide={(): void => this.setState({addEditModalShowed: false})}>
					<Modal.Header closeButton>
						{this.state.user == DEFAULT_USER ? "Dodaj konto" : "Edytuj konto"}
					</Modal.Header>
					<Modal.Body>
						<label htmlFor="loginBox" className="me-3">Login</label>
						<input value={this.state.user.login} id="loginBox" type="text"
							onChange={(e): void => {
								this.setState({
									user: {
										...this.state.user,
										login: e.target.value
									},
								});
							}} />
						<br/>
						<label htmlFor="nameBox" className="me-3 mt-2">Imię i nazwisko</label>
						<input value={this.state.user.fullname} id="nameBox" type="text"
							onChange={(e): void => {
								this.setState({
									user: {
										...this.state.user,
										fullname: e.target.value
									},
								});
							}} />
						<br/>
						<label htmlFor="passwordBox" className="me-3 mt-2">Hasło</label>
						<input value={this.state.user.password} id="passwordBox" type="password"
							onChange={(e): void => {
								this.setState({
									user: {
										...this.state.user,
										password: e.target.value
									},
								});
							}} />
						<br/>
						<label htmlFor="adminBox" className="me-3 mt-2">Administrator</label>
						<input checked={this.state.user.isAdmin} id="adminBox" type="checkbox"
							onChange={(e): void => {
								this.setState({
									user: {
										...this.state.user,
										isAdmin: e.target.checked
									},
								});
							}} />
					</Modal.Body>
					<Modal.Footer>
						<Button>Ok</Button>
						<Button onClick={(): void => this.setState({addEditModalShowed: false})}>Anuluj</Button>
					</Modal.Footer>
				</Modal>
			</div>
		);
	}
}

export default UsersTable;