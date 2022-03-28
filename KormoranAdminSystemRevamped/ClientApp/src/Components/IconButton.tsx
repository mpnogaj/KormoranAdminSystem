import "./IconButton.css";

interface IProps{
	icon: any,
	onClick: Function;
}

const IconButton = (props: IProps) => {
	return (
		<a className="btn iconButton" onClick={() => props.onClick()}>
			{props.icon}
		</a>
	)
}

export default IconButton;