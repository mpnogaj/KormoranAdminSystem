import React from "react";
import UsersTable from "../../../Components/UsersTable";

class Users extends React.Component {
	render(): JSX.Element {
		return (
			<div className="container mt-3">
				<div className="logo-container">
					<p>Zarządzanie turniejami</p>
				</div>
				<UsersTable/>
			</div>
		);
	}
}

export default Users;