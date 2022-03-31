import React from 'react';

class AbfListItem {
    constructor(abfID, childCount, comment, color) {
        this.abfID = abfID;
        this.childCount = childCount;
        this.comment = comment;
        this.color = color;
    }

    getHtml() {
        return (
            <div key={this.abfID} style={{ fontFamily: 'monospace', fontSize: '.8em', }}>

                <span style={{ backgroundColor: this.color }}>
                    {this.abfID}
                </span>

                <div style={{ display: 'inline-block' }}>
                    &nbsp;({this.childCount})&nbsp;
                </div>

                <div style={{ display: 'inline-block' }}>
                    {this.comment}
                </div>

            </div>
        );
    }
}

class AbfParentList extends React.Component {

    componentDidMount() {
        this.fetchParents();
    }

    state = {
        parents: null,
        folder: "folder not set",
    };

    parentsFromJson = (json) =>
        Object.keys(json).map(parentID => {
            return new AbfListItem(
                parentID,
                json[parentID]["childCount"],
                json[parentID]["comment"],
                json[parentID]["color"]
            );
        });

    fetchParents() {
        this.setState({ folder: "Loading..." })
        const folderPath = 'X:/Data/SD/practice/Scott/2022/2022-01-04-AON';
        const url = `http://192.168.1.9/abf-browser/api/v3/abf-menu-items/?folder=` + folderPath;
        fetch(url)
            .then(response => response.json())
            .then(json => {
                this.setState({
                    parents: this.parentsFromJson(json),
                    folder: folderPath,
                });
            })
    }

    render() {
        return (
            <div>
                <h1>{this.state.folder}</h1>
                <ul>
                    {this.state.parents?.map(parent => parent.getHtml())}
                </ul>
            </div>
        )
    }
}

export default AbfParentList;