import React from "react";
import ILog from "../Models/ILog";

interface IProps {
    item: ILog
}

class LogRow extends React.Component<IProps>{
    render(): JSX.Element {
        return (
            <tr>
                <td>{this.props.item.level}</td>
                <td>{this.props.item.date}</td>
                <td>{this.props.item.author}</td>
                <td>{this.props.item.action}</td>
            </tr>
        );
    }
}

export default LogRow;