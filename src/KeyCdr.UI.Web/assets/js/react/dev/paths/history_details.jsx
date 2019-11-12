import React from 'react';
import ReactDOM from 'react-dom';
import { Redirect} from 'react-router-dom'

import ContentContainer from '../content.jsx';
import Loading from '../loading.jsx';
import SecureRouteComponent from '../secure_route_component.jsx';

class HistoryDetails extends SecureRouteComponent {
    constructor(props) {
        super(props);

        this.state = {
            isLoading: true,
            data: null
        }

        /* there has to be a better way to do this, but I don't
        want to load these files for every page, just the handful
        that need a grid */
        this.loadCss('https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css')
            .then(() => {
                this.loadCss('https://cdn.datatables.net/rowgroup/1.1.1/css/rowGroup.dataTables.min.css');
            });

        this.loadScript('https://code.jquery.com/jquery-3.4.1.min.js')
            .then(() => {
                this.loadScript('https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js')
                    .then(() => {
                        this.loadScript('https://cdn.datatables.net/rowgroup/1.1.1/js/dataTables.rowGroup.min.js')
                            .then(() => {
                                this.setState({ isLoading: false});
                                /*this.getUserHistory()
                                    .then(resp => {
                                        this.setState({ isLoading: false, data: resp });
                                        this.initGrid();
                                    });
                                */
                            });
                    });
            });
    }

    initGrid() {

    }

    getUserHistory() {
        var token = this.auth.getCurrentToken();
        return fetch('/api/User/History', {
                method: "Post",
                headers: { 'Content-Type': 'application/json', Accept: 'application/json' }
                , body: JSON.stringify(token)
            })
            .then(resp => resp.json())
            .then(data => {
                return Promise.resolve(data);
            });
    }

    render() {
        if (this.authenticationComplete()) {

            return (
                <ContentContainer authed={true}>
                    <h2>History Details</h2>

                    <div className="content_row">
                        <Loading showLoading={this.state.isLoading} />
                        
                    </div>
                </ContentContainer>
            );
        }
        else {
            return (
                super.render()
            )
        }
    }
}

export default HistoryDetails;