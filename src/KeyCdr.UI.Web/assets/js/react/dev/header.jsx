import React from 'react';
import ReactDOM from 'react-dom';
import {
    Link
} from "react-router-dom";

import HeaderNavBar from './header-nav-bar.jsx';

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
                            <a href="test.com" className="title">Key Commander</a> 
                            <div className="title_sub">measure, improve and analyze typing speed and accuracy {this.props.loc}</div>
                        </div>
                        <HeaderNavBar />
                        {/*
                        <div id="nav_container">
                            <ul>
                                <HeaderNavBarLink to="/Index" >Home</HeaderNavBarLink>
                                <HeaderNavBarLink to="/Index" >My Account</HeaderNavBarLink>
                                <HeaderNavBarLink to="/About" >About</HeaderNavBarLink>
                            </ul>
                        </div>
                        */}
                        <div className="clear_both"></div>
                    </div>

                    <div className="clear_both"></div>
                </div>
            </React.Fragment>
        );
    }
}

export default Header;
