import React from "react";
import TournamentRow from "../../Components/TournamentRow";
import ITournamentRow from "../../Models/ITournamentRow";
import axios from "axios";
import ITournamentsResponse from "../../Models/Responses/ITournamentsResponse";

interface IState{
	tournaments: Array<ITournamentRow>
}

class Guest extends React.Component<any, IState>{
	private timerID: number;
	constructor(props: any) {
		super(props);
		this.state = {
			tournaments: []
		};
		this.timerID = 0;
	}
	
	componentWillUnmount() {
		window.clearTimeout(this.timerID);
	}

	componentDidMount() {
		this.timerID = window.setInterval( async () => {
			const response = await axios.get<ITournamentsResponse>("api/Tournaments", {
				params: []
			});
			console.log(response);
			if(response.status === 200){
				console.log("ustawiam");
				console.log(response.data.tournaments);
				this.setState<"tournaments">({tournaments: response.data.tournaments});
			}
		}, 5000);
	}

	render(){
		return (
			<div className="container mt-3">
			<div className="logo-container">
				<p>Podgląd turniejów na żywo</p>
			</div>
			<table className="table table-hover table-bordered">
				<thead>
					<tr>
						<th>Nazwa</th>
						<th>Status</th>
						<th>Dyscyplina</th>
						<th>Typ turnieju</th>
						<th>Akcja</th>
					</tr>
				</thead>
				<tbody className="align-middle">
					{
						this.state.tournaments.length > 0
							? 
							this.state.tournaments.map((val) => {
								return (
									<TournamentRow name={val.name} state={val.state} type={val.type} 
												   discipline={val.discipline}/>
								);
							}) 
							:
							<tr>
								<td style={{textAlign: "center"}} colSpan={5}>Ładowanie...</td>
							</tr>
					}
				</tbody>
			</table>
		</div>
		);
	}
}

export default Guest;