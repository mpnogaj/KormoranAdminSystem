import React from "react";
import "./IconButton.css";

interface IProps {
    icon: JSX.Element,
    onClick: () => void;
}

const IconButton = (props: IProps): JSX.Element => {
    return (
        <a className="btn iconButton" onClick={(): void => props.onClick()}>
            {props.icon}
        </a>
    );
};

export default IconButton;