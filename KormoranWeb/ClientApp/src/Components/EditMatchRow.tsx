import React from "react";
import ITeam from "../Models/ITeam";
import { Trash } from "react-bootstrap-icons";
import IconButton from "./IconButton";
import IState from "../Models/IState";
import IMatch from "../Models/IMatch";

interface ICompProps {
    match: IMatch,
    teams: Array<ITeam>,
    states: Array<IState>,
    onUpdate: (targetId: number, target: number, data: number) => void;
}

class EditMatchRow extends React.Component<ICompProps>{
    updateTeam = (newId: number, target: number): void => {
        this.props.onUpdate(this.props.match.matchId, target, newId);
    };

    updateScore = (newScore: number, target: number): void => {
        this.props.onUpdate(this.props.match.matchId, target + 2, newScore);
    };

    updateState = (newId: number): void => {
        this.props.onUpdate(this.props.match.matchId, 6, newId);
    };

    renderTeams = (): Array<JSX.Element> => {
        return (
            this.props.teams.map((team) => {
                return (
                    <option key={team.id} value={team.id}>
                        {team.name}
                    </option>
                );
            })
        );
    };

    renderStates = (): Array<JSX.Element> => {
        return (
            this.props.states.map((state) => {
                return (
                    <option key={state.id} value={state.id}>
                        {state.name}
                    </option>
                );
            })
        );
    };

    render(): JSX.Element {
        return (
            <tr className="align-middle">
                <th>{this.props.match.matchId == 0 ? "-" : this.props.match.matchId}</th>
                <th>
                    <select onChange={(e): void => {
                        this.updateState(parseInt(e.target.value));
                    }} value={this.props.match.state.id}>
                        <option value={0}>-</option>
                        {this.renderStates()}
                    </select>
                </th>
                <th>
                    <select onChange={(e): void => {
                        this.updateTeam(parseInt(e.target.value), 1);
                    }} value={this.props.match.team1.id}>
                        <option value={0}>-</option>
                        {this.renderTeams()}
                    </select>
                </th>
                <th>
                    <select onChange={(e): void => {
                        this.updateTeam(parseInt(e.target.value), 2);
                    }} value={this.props.match.team2.id}>
                        <option value={0}>-</option>
                        {this.renderTeams()}
                    </select>
                </th>
                <th>
                    <input type="number" value={this.props.match.team1Score}
                        onChange={(e): void => {
                            const val = parseInt(e.target.value);
                            e.target.value = val.toString();
                            this.updateScore(isNaN(val) ? 0 : val, 1);
                        }}
                    />
                </th>
                <th>
                    <input type="number" value={this.props.match.team2Score}
                        onChange={(e): void => {
                            const val = parseInt(e.target.value);
                            e.target.value = val.toString();
                            this.updateScore(isNaN(val) ? 0 : val, 2);
                        }}
                    />
                </th>
                <th>
                    <IconButton icon={<Trash height={24} width={24} />} onClick={(): void => {
                        this.props.onUpdate(this.props.match.matchId, 5, -1);
                    }} />
                </th>
            </tr>
        );
    }
}

export default EditMatchRow;