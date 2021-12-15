import React from "react";
import TournamentsTable from "../../Components/TournamentsTable";

class Guest extends React.Component<any, any>{
	render(){
		return (
			<div className="container mt-3">
				<div className="logo-container">
					<p>Podgląd turniejów na żywo</p>
				</div>
				<TournamentsTable/>
			</div>
		);
	}
}

export default Guest;