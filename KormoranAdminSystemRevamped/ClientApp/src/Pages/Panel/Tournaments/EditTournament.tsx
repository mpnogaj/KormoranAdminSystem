import React from "react";
import withParams from "../../../Helpers/HOC";

interface IParams{
    id: number
}

interface IProps{
    params: IParams
}

class EditTournament extends React.Component<IProps, any>{
	render() {
		return(
			<div className="container mt-3">
				<div className="logo-container">
					<p>Edytuj turniej</p>
				</div>
				<h1>{this.props.params.id}</h1>
			</div>
		)
	}
}

export default withParams(EditTournament);