import React from "react";
import LogTable from "../../../Components/LogsTable";

class Logs extends React.Component{
	render(): JSX.Element {
		return (
			<div className="container mt-3">
				<div className="logo-container">
					<p>Logi</p>
				</div>
				<LogTable />
			</div>
		);
	}
}

export default Logs;