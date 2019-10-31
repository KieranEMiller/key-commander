import React from 'react';
import ReactDOM from 'react-dom';
import { Link } from "react-router-dom";

class HeaderNavBar extends React.Component {
    constructor(params) {
        super(params);
        this.state = {
            routes: [
                { route: "/Index", display: "Home", active: false },
                { route: "/Account", display: "My Account", active: false },
                { route: "/About", display: "About", active: false }
            ]
        };
    };
    clickHandler(route) {
        this.setState(state => {
            const list = state.routes.map((item, j) => {
                item.active = (item.route === route);
                return item;
            });
            return {list};
        });
    };
    render() {
        return (
            <React.Fragment>
                <div id="nav_container">
                    <ul>
                        {this.state.routes.map((route, index) => {
                            var css = (route.active) ? 'current' : '';
                            return (
                                <li key={index} className={css}>
                                    <Link to={route.route} onClick={() => this.clickHandler(route.route)}>{route.display}</Link>
                                </li>
                            )   
                        })}
                    </ul>
                </div>
            </React.Fragment>
        );
    }
}

export default HeaderNavBar;