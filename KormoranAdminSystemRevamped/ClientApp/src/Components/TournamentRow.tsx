import React from "react";

interface IProps{
    name: string,
    state: string,
    type: string,
    discipline: string
}

class TournamentRow extends React.Component<IProps, any>{
    render() {
        return (
            <tr>
                <td>{this.props.name}</td>
                <td>
                    <span className="badge bg-success">{this.props.state}</span>
                </td>
                <td>{this.props.discipline}</td>
                <td>{this.props.type}</td>
                <td>
                    <a href="#" className="btn btn-success">Podgląd</a>
                </td>
            </tr>
        );
    }

}
export default TournamentRow