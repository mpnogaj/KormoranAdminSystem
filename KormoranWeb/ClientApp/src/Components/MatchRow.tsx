import React from "react";
import { Badge } from "react-bootstrap";
import IMatch from "../Models/IMatch";

interface IProps {
    match: IMatch,
}

class TournamentRow extends React.Component<IProps>{
    render(): JSX.Element {
        return (
            <tr>
                <td>{this.props.match.matchId}</td>
                <td>
                    <Badge bg="success">{this.props.match.state.name}</Badge>
                </td>
                <td>{this.props.match.team1.name}</td>
                <td>{this.props.match.team2.name}</td>
                <td>{this.props.match.winner.name}</td>
                <td>{this.props.match.team1Score}:{this.props.match.team2Score}</td>
            </tr>
        );
    }
}
export default TournamentRow;