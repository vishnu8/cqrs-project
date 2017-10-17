﻿import * as React from "react";
import * as ReactDom from "react-dom";
import { observable } from "mobx";
import { observer } from "mobx-react";
import { IClient } from "./interface";
import clientStore from "./store";

interface IEditClientProps {
    clientId: string
}

@observer
class EditClient extends React.Component<IEditClientProps> {
    @observable client: IClient = {
        name: "",
        description: ""
    } as IClient;

    constructor(props) {
        super(props);
        
        clientStore.getClient(this.props.clientId).then(client => {
            this.client = client;
        });
    }

    async updateClient(client: IClient) {
        await clientStore.updateClient(client);
        window.location.href = "/clients";
    }

    render() {
        return (
            <form>
                <div className="form-horizontal">
                    <hr />
                    <div className="form-group">
                        <label className="col-md-2 control-label"></label>
                        <div className="col-md-10">
                            <input
                                className="form-control"
                                value={this.client.name}
                                onChange={e => this.client.name = e.target.value} />
                        </div>
                    </div>

                    <div className="form-group">
                        <label className="col-md-2 control-label"></label>
                        <div className="col-md-10">
                            <input
                                className="form-control"
                                value={this.client.description}
                                onChange={e => this.client.description = e.target.value} />
                        </div>
                    </div>

                    <div className="form-group">
                        <div className="col-md-offset-2 col-md-10">
                            <button type="button" className="btn btn-success" onClick={() => this.updateClient(this.client)}>
                                Save
                            </button>
                            <a href="/clients" className="btn btn-info">Back to List</a>
                        </div>
                    </div>
                </div>
            </form>
        );
    }
}

const html = document.getElementById("main");

ReactDom.render(
    <EditClient clientId={html.getAttribute("data-clientid")}></EditClient>,
    html
);