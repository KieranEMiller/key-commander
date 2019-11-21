import React from 'react';
import ReactDOM from 'react-dom';
import { Link } from "react-router-dom";

import HeaderNavBar from './components/header-nav-bar.jsx';

class Header extends React.Component {
    constructor(props) {
        super(props);
    };
    render() {
        return (
            <React.Fragment>
                <div id="header">
                    <div className="top_bar"></div>
                    <div id="header_contents">
                        <div id="title_block">
                            <Link to="/" className="title">Key Commander</Link>
                            <div className="title_sub">measure, improve and analyze typing speed and accuracy {this.props.loc}</div>
                        </div>
                        <HeaderNavBar IsAuthed={this.props.IsAuthed} appLogin={this.props.appLogin} appLogout={this.props.appLogout}/>
                        <div className="clear_both"></div>
                    </div>

                    <div className="clear_both"></div>
                </div>
            </React.Fragment>
        );
    }
}

export default Header;
