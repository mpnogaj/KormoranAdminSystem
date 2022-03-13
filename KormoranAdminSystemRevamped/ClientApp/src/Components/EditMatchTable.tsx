import React from "react";
import { Table, Button } from "react-bootstrap";
import IMatch from "../Models/IMatch";
import ITeam from "../Models/ITeam";
import EditMatchRow from "./EditMatchRow";
import { nanoid } from "nanoid";
import IState from "../Models/IState";
import { IRowData } from "../Pages/Panel/Tournaments/EditTournament";
import { binsearch } from "../Helpers/Essentials";

interface ICompProps{
	teams: Array<ITeam>,
	states: Array<IState>,
	matchesData: Array<IRowData>,
	updateMatches: (newData: Array<IRowData>) => void;
}

class EditMatchTable extends React.Component<ICompProps, any>{
	
	constructor(props: ICompProps){
		super(props);
	}

	private defaultMatchData() : IRowData {
		return {
			id: nanoid(),
			matchId: 0,
			stateId: 0,
			team1: 0,
			team2: 0,
			team1Score: 0,
			team2Score: 0
		}
	}

	render() {
		return(
			<div>
				<span>Mecze turnijowe: </span><br />
				<span>{}</span>
				<Button onClick={() => {
					const newData = this.props.matchesData;
					newData.push(this.defaultMatchData())
					this.props.updateMatches(newData);
				}}>Dodaj nowy</Button>
				<Table className="mt-3" responsive={true} bordered={true}>
					<thead>
						<tr>
							<th>Id</th>
							<th>Stan</th>
							<th>Drużyna 1</th>
							<th>Drużyna 2</th>
							<th>Wynik drużyny 1</th>
							<th>Wynik drużyny 2</th>
							<th>Zwyciezca</th>
							<th></th>
						</tr>
					</thead>
					<tbody>
						{
							this.props.matchesData.map((data: IRowData, index) => {
								return (
									<EditMatchRow 
										match={data}
										teams={this.props.teams}
										states={this.props.states}
										id={index} key={data.id}
										team1={data.team1} 
										team2={data.team2} 
										team1Pts={data.team1Score} team2Pts={data.team2Score}
										state={data.stateId}
										onUpdate={(targetId, targetVal, value) => {
											const newData: Array<IRowData> = this.props.matchesData.slice();
											switch(targetVal){
												case 1:
													newData[targetId].team1 = value;
													break;
												case 2:
													newData[targetId].team2 = value;
													break;
												case 3:
													newData[targetId].team1Score = value;
													break;
												case 4:
													newData[targetId].team2Score = value;
													break;
												case 5:
													newData.splice(targetId, 1);
													break;
												case 6:
													newData[targetId].stateId = value;
													break;
												default:
													console.error("Invalid targetVal parameter! Passed: " + targetVal);
													break;
											}
											this.props.updateMatches(newData);
										}}
									/>
								);
							})
						}
					</tbody>
				</Table>
			</div>
		)
	}
}

export default EditMatchTable