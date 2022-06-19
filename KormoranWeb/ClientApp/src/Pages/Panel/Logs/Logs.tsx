import React from "react";
import { Container } from "react-bootstrap";
import LogsTable from "../../../Components/LogsTable";

class UsersTable extends React.Component {
	render() : JSX.Element {
		return (
			<Container className="mt-3">
				<div className="logo-container">
					<p>Logi</p>
				</div>
				<LogsTable/>
			</Container>
		);
	}
} 

export default UsersTable;