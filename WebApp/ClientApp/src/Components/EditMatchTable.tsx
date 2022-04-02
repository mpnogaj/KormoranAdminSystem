import React from "react";
import { Table, Button } from "react-bootstrap";
import ITeam from "../Models/ITeam";
import EditMatchRow from "./EditMatchRow";
import IState from "../Models/IState";
import IMatch from "../Models/IMatch";
import { binsearchInd } from "../Helpers/Essentials";
import {customAlphabet} from "nanoid";

interface IProps {
	teams: Array<ITeam>,
	states: Array<IState>,
	matches: Array<IMatch>,
	tournamentId: number,
	updateMatches: (newData: Array<IMatch>) => void;
}

class EditMatchTable extends React.Component<IProps>{
	private readonly generator;

	constructor(props: IProps){
		super(props);
		this.generator = customAlphabet("123456789", 6);
	}

	makeTeam = (id: number): ITeam => {
		return {
			id: id,
			name: "UNUSED",
			tournamentId: this.props.tournamentId
		};
	};

	render(): JSX.Element {
		return (
			<div>
				<span>Mecze turnijowe: </span><br />
				<span>{ }</span>
				<Button onClick={(): void => {
					const newData = this.props.matches;
					newData.push({
						matchId: parseInt(this.generator()),
						winner: {
							id: 0,
							name: "UNUSED",
							tournamentId: this.props.tournamentId
						},
						team1: {
							id: 0,
							name: "UNUSED",
							tournamentId: this.props.tournamentId
						},
						team2: {
							id: 0,
							name: "UNUSED",
							tournamentId: this.props.tournamentId
						},
						state: {
							id: 0,
							name: "UNUSED"
						},
						team1Score: 0,
						team2Score: 0
					});
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
							<th></th>
						</tr>
					</thead>
					<tbody>
						{
							this.props.matches.map((data: IMatch) => {
								return (
									<EditMatchRow
										key={data.matchId}
										match={data}
										teams={this.props.teams}
										states={this.props.states}
										onUpdate={(targetId, targetVal, value): void => {
											const newData: Array<IMatch> = this.props.matches.slice();
											const ind: number = binsearchInd(newData, x => x.matchId - targetId);
											switch (targetVal) {
												case 1:
													newData[ind].team1 = this.makeTeam(value);
													break;
												case 2:
													newData[ind].team2 = this.makeTeam(value);
													break;
												case 3:
													newData[ind].team1Score = value;
													break;
												case 4:
													newData[ind].team2Score = value;
													break;
												case 5:
													newData.splice(ind, 1);
													break;
												case 6:
													newData[ind].state = {
														id: value,
														name: "UNUSED"
													};
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
		);
	}
}

export default EditMatchTable;