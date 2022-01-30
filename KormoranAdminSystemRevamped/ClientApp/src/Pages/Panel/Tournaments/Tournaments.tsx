import React from "react";
import TournamentsTable from "../../../Components/TournamentsTable";

class Tournaments extends React.Component<any, any>{
	render() {
		return(
			<div className="container mt-3">
				<div className="logo-container">
					<p>Zarządzanie turniejami</p>
				</div>
				<TournamentsTable allowEdit={true} />
			</div>
		)
	}
}

export default Tournaments;