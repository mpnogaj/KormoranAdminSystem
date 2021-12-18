import React from "react";
import "./Panel.css";

class Panel extends React.Component<any, any>{
	render() {
		return (
			<div className="container-fluid overflow-hidden">
				<div className="row vh-100 overflow-auto">
					<div className="col-12 col-sm-3 col-xl-2 px-sm-2 px-0 bg-dark d-flex sticky-top">
						<div
							className="d-flex flex-sm-column flex-row flex-grow-1 align-items-center align-items-sm-start px-3 pt-2 text-white">
							<a href="/"
							   className="d-flex align-items-center pb-sm-3 mb-md-0 me-md-auto text-white text-decoration-none">
								<span className="fs-5">B<span className="d-none d-sm-inline">rand</span></span>
							</a>
							<ul className="nav nav-pills flex-sm-column flex-row flex-nowrap flex-shrink-1 flex-sm-grow-0 flex-grow-1 mb-sm-auto mb-0 justify-content-center align-items-center align-items-sm-start"
							    id="menu">
								<li className="nav-item">
									<a href="#" className="nav-link px-sm-0 px-2">
										<i className="fs-5 bi-house"/><span
										className="ms-1 d-none d-sm-inline">Home</span>
									</a>
								</li>
								<li>
									<a href="#submenu1" data-bs-toggle="collapse" className="nav-link px-sm-0 px-2">
										<i className="fs-5 bi-speedometer2"/><span
										className="ms-1 d-none d-sm-inline">Dashboard</span> </a>
								</li>
								<li>
									<a href="#" className="nav-link px-sm-0 px-2">
										<i className="fs-5 bi-table"/><span
										className="ms-1 d-none d-sm-inline">Orders</span></a>
								</li>
								<li className="dropdown">
									<a href="#" className="nav-link dropdown-toggle px-sm-0 px-1" id="dropdown"
									   data-bs-toggle="dropdown" aria-expanded="false">
										<i className="fs-5 bi-bootstrap"/><span
										className="ms-1 d-none d-sm-inline">Bootstrap</span>
									</a>
									<ul className="dropdown-menu dropdown-menu-dark text-small shadow"
									    aria-labelledby="dropdown">
										<li><a className="dropdown-item" href="#">New project...</a></li>
										<li><a className="dropdown-item" href="#">Settings</a></li>
										<li><a className="dropdown-item" href="#">Profile</a></li>
										<li>
											<hr className="dropdown-divider"/>
										</li>
										<li><a className="dropdown-item" href="#">Sign out</a></li>
									</ul>
								</li>
								<li>
									<a href="#" className="nav-link px-sm-0 px-2">
										<i className="fs-5 bi-grid"/><span
										className="ms-1 d-none d-sm-inline">Products</span></a>
								</li>
								<li>
									<a href="#" className="nav-link px-sm-0 px-2">
										<i className="fs-5 bi-people"/><span
										className="ms-1 d-none d-sm-inline">Customers</span> </a>
								</li>
							</ul>
							<div className="dropdown py-sm-4 mt-sm-auto ms-auto ms-sm-0 flex-shrink-1">
								<a href="#"
								   className="d-flex align-items-center text-white text-decoration-none dropdown-toggle"
								   id="dropdownUser1" data-bs-toggle="dropdown" aria-expanded="false">
								</a>
								<ul className="dropdown-menu dropdown-menu-dark text-small shadow"
								    aria-labelledby="dropdownUser1">
									<li><a className="dropdown-item" href="#">New project...</a></li>
									<li><a className="dropdown-item" href="#">Settings</a></li>
									<li><a className="dropdown-item" href="#">Profile</a></li>
									<li>
										<hr className="dropdown-divider"/>
									</li>
									<li><a className="dropdown-item" href="#">Sign out</a></li>
								</ul>
							</div>
						</div>
					</div>
					<div className="col d-flex flex-column h-sm-100">
						<main className="row overflow-auto">
							<div className="col pt-4">
								<h3>Vertical Sidebar that switches to Horizontal Navbar</h3>
								<p className="lead">An example multi-level sidebar with collasible menu items. The menu
									functions like an "accordion" where only a single menu is be open at a time.</p>
								<hr/>
								<h3>More content...</h3>
								<p>Sriracha biodiesel taxidermy organic post-ironic, Intelligentsia salvia mustache 90's
									code editing brunch. Butcher polaroid VHS art party, hashtag Brooklyn deep v PBR
									narwhal sustainable mixtape swag wolf squid tote bag. Tote bag cronut semiotics, raw
									denim deep v taxidermy messenger bag. Tofu YOLO Etsy, direct trade ethical Odd
									Future jean shorts paleo. Forage Shoreditch tousled aesthetic irony, street art
									organic Bushwick artisan cliche semiotics ugh synth chillwave meditation. Shabby
									chic lomo plaid vinyl chambray Vice. Vice sustainable cardigan, Williamsburg master
									cleanse hella DIY 90's blog.</p>
								<p>Ethical Kickstarter PBR asymmetrical lo-fi. Dreamcatcher street art Carles, stumptown
									gluten-free Kickstarter artisan Wes Anderson wolf pug. Godard sustainable you
									probably haven't heard of them, vegan farm-to-table Williamsburg slow-carb readymade
									disrupt deep v. Meggings seitan Wes Anderson semiotics, cliche American Apparel
									whatever. Helvetica cray plaid, vegan brunch Banksy leggings +1 direct trade.
									Wayfarers codeply PBR selfies. Banh mi McSweeney's Shoreditch selfies, forage
									fingerstache food truck occupy YOLO Pitchfork fixie iPhone fanny pack art party
									Portland.</p>
							</div>
						</main>
						<footer className="row bg-light py-4 mt-auto">
							<div className="col"> Footer content here...</div>
						</footer>
					</div>
				</div>
			</div>
		)
	}
}

export default Panel;