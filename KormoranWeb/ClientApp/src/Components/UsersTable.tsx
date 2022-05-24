import axios, { AxiosResponse } from "axios";
import React from "react";
import { Button, Modal, Table } from "react-bootstrap";
import { Empty } from "../Helpers/Aliases";
import { DEFAULT_TIMEOUT, DownloadManager } from "../Helpers/DownloadManager";
import { ADD_EDIT_USER, DELETE_USER, GET_USERS } from "../Helpers/Endpoints";
import { binsearch } from "../Helpers/Essentials";
import { addEditUserFromIUser, DEFAULT_ADD_EDIT_USER, IAddEditUser } from "../Models/IRequests";
import { IAdminCheckResponse, ICollectionResponse } from "../Models/IResponses";
import IUser, { DEFAULT_USER } from "../Models/IUser";
import UserRow from "./UserRow";

interface IState {
	isAdmin: boolean,
	isLoading: boolean,
	saveEnabled: boolean,
	user: IAddEditUser,
	addEditModalShowed: boolean,
	users: Array<IUser>;
}

class UsersTable extends React.Component<Empty, IState> {
	readonly userDownloader: DownloadManager<ICollectionResponse<IUser>, null>;
	private isEdit = false;

	constructor(props: Empty) {
		super(props);
		this.userDownloader = new DownloadManager<ICollectionResponse<IUser>, null>(
			GET_USERS, DEFAULT_TIMEOUT, (response) => {
				this.setState({ users: response.collection, isLoading: false });
			});
		this.state = {
			isLoading: false,
			isAdmin: false,
			user: DEFAULT_ADD_EDIT_USER,
			saveEnabled: false,
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
		const user: IAddEditUser = bsRes == undefined ? DEFAULT_ADD_EDIT_USER : addEditUserFromIUser(bsRes);
		this.isEdit = user != DEFAULT_ADD_EDIT_USER;
		console.log(this.isEdit);
		this.setState({
			addEditModalShowed: true,
			user: user,
			saveEnabled: this.validateData(user)
		});
	};

	saveChanges = async (): Promise<void> => {
		console.log(this.state.user);
		const res = await axios.post(ADD_EDIT_USER, this.state.user);
		console.log(res);
		this.setState({ addEditModalShowed: false });
	};

	validateData = (user: IAddEditUser): boolean => this.isEdit ? user.fullname != "" && user.login != "" : user.fullname != "" && user.login != "" && user.password != "";

	deleteUser = async (userId: number): Promise<void> => {
		const res = await axios.delete(DELETE_USER, {
			params: {
				userId: userId
			}
		});
		console.log(res);
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
										<UserRow key={u.id} user={u} showEdit={this.state.isAdmin}
											onDeleteClicked={async (): Promise<void> => this.deleteUser(u.id)}
											onEditClicked={(): void => this.showAddEditModal(u.id)} />
									);
								})
						}
					</tbody>
				</Table>
				{
					this.state.isAdmin ? <Button onClick={(): void => this.showAddEditModal(0)}>Dodaj konto</Button> : null
				}
				<Modal show={this.state.addEditModalShowed}
					onHide={(): void => this.setState({ addEditModalShowed: false })}>
					<Modal.Header closeButton>
						{this.state.user == DEFAULT_USER ? "Dodaj konto" : "Edytuj konto"}
					</Modal.Header>
					<Modal.Body>
						<label htmlFor="loginBox" className="me-3">Login</label>
						<input value={this.state.user.login} id="loginBox" type="text"
							onChange={(e): void => {
								const newUser: IAddEditUser = {
									...this.state.user,
									login: e.target.value
								};
								this.setState({
									user: newUser,
									saveEnabled: this.validateData(newUser)
								});
							}} />
						<br />
						<label htmlFor="nameBox" className="me-3 mt-2">Imię i nazwisko</label>
						<input value={this.state.user.fullname} id="nameBox" type="text"
							onChange={(e): void => {
								const newUser: IAddEditUser = {
									...this.state.user,
									fullname: e.target.value
								};
								this.setState({
									user: newUser,
									saveEnabled: this.validateData(newUser)
								});
							}} />
						<br />
						<label htmlFor="passwordBox" className="me-3 mt-2">Hasło (przy edycji można zostawić pole puste)</label>
						<input value={this.state.user.password} id="passwordBox" type="password"
							onChange={(e): void => {
								const newUser: IAddEditUser = {
									...this.state.user,
									password: e.target.value
								};
								this.setState({
									user: newUser,
									saveEnabled: this.validateData(newUser)
								});
							}} />
						<br />
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
						<Button disabled={!this.state.saveEnabled} onClick={this.saveChanges}>Ok</Button>
						<Button onClick={(): void => this.setState({ addEditModalShowed: false })}>Anuluj</Button>
					</Modal.Footer>
				</Modal>
			</div>
		);
	}
}

export default UsersTable;