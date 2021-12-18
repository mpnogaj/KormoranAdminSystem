import React from "react";
import {Col, Container, Row} from "react-bootstrap";

class Panel extends React.Component<any, any>{
	render() {
		return (
			<Container fluid={true}>
				<Row>
					<Col sm={2}  style={{background: "red"}}>
						<p>LEWY PANEL</p>
					</Col>
					<Col sm={10} style={{ background: "blue"}}>
						<p>PANEL</p>
					</Col>
				</Row>
			</Container>
		)
	}
}

export default Panel;