import React from 'react';
import ReactDOM from 'react-dom';
import { Redirect} from 'react-router-dom'

import ContentContainer from '../content.jsx';
import Loading from '../loading.jsx';
import SecureRouteComponent from '../secure_route_component.jsx';

class History extends SecureRouteComponent {
    constructor(props) {
        super(props);

        this.state = {
            isLoading: true,
            data: null
        }

        /* there has to be a better way to do this, but I don't
        want to load these files for every page, just the handful
        that need a grid */
        this.loadCss('https://cdnjs.cloudflare.com/ajax/libs/jsgrid/1.5.3/jsgrid.min.css')
            .then(() => {
                this.loadCss('https://cdnjs.cloudflare.com/ajax/libs/jsgrid/1.5.3/jsgrid-theme.min.css');
            });

        this.loadScript('https://code.jquery.com/jquery-3.4.1.min.js')
            .then(() => {
                this.loadScript('https://cdnjs.cloudflare.com/ajax/libs/jsgrid/1.5.3/jsgrid.min.js')
                    .then(() => {
                        this.getUserHistory()
                            .then(resp => {
                                this.setState({ isLoading: false, data: resp });
                                this.initGrid();
                            }); 
                    });
            });
    }

    initGrid() {
        $("#grid").jsGrid({
            width: "100%",
            height: "400px",

            inserting: false,
            editing: false,
            sorting: true,
            paging: false,

            data: this.state.data,

            rowClass: "grid_row",

            fields: [
                /*{ name: "UserId", type: "text", width: 50, validate: "required" },
                { name: "SessionId", type: "text", width: 50 },*/

                { name: "CreateDate", type: "text"},
                { name: "SequenceCount", type: "number"},
                { name: "SourceType", type: "text"},
                { name: "SourceKey", type: "text"}
            ]
        });
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

            let historyData = this.state && this.state.data && this.state.data.length > 0 ?
                this.state.data.map(item => {
                    return (
                        <tr key={item.SessionId}>
                            <td>{item.CreateDate}</td>
                            <td>{item.SequenceCount}</td>
                        </tr>
                    )
                })
                : <tr><td>No history found...</td></tr>;

            return (
                <ContentContainer authed={true}>
                    <h2>Session History</h2>

                    <div className="content_row">
                        <Loading showLoading={this.state.isLoading} />
                        <div id="grid"></div>
                        {/*
                        <table>
                            <thead>
                                <th>
                                    <td>CreateDate</td>
                                    <td>SequenceCount</td>
                                </th>
                            </thead>
                            <tbody>
                                {historyData} 
                            </tbody>
                        </table>
                        */}
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

export default History;