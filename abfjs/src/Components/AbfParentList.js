import React from 'react';
import AbfParent from '../Models/AbfParent';

class AbfParentList extends React.Component {

    state = {
        parents: null,
        folder: "folder not set",
        parents: this.getInitialParents(),
    };

    getInitialParents() {
        let parents = [];
        parents.push(new AbfParent("x:/folder/path1.abf"));
        parents.push(new AbfParent("x:/folder/path2.abf"));
        parents.push(new AbfParent("x:/folder/path3.abf"));
        return parents;
    }

    parentsFromJson(json) {

        let parents = [];
        json.filenames.forEach(filename => {
            const abfPath = json.folder + "/" + filename;
            const parent = new AbfParent(abfPath);
            parents.push(parent);
        });

        return parents;
    }

    fetchParents() {
        const folderPath = `X:/Data/SD/practice/Scott/2022/2022-01-04-AON`
        const url = `http://192.168.1.9/abf-browser/api/v3/folder-info/?folder=` + folderPath;
        fetch(url)
            .then(response => response.json())
            .then(json => {
                this.setState({
                    parents: this.parentsFromJson(json),
                    folder: folderPath,
                });
            })
    }

    getParentHtml(parent) {
        return (
            <div key={parent.path}>
                <span style={{ backgroundColor: parent.color, paddingLeft: 3, paddingRight: 3 }}>
                    {parent.abfID}
                </span>
                <span>
                    ({parent.childAbfs.length})
                </span>
                <span style={{ paddingLeft: 5, opacity: .5 }}>
                    {parent.comment}
                </span>
            </div>
        );
    }

    render() {
        return (
            <div>
                <h1>{this.state.folder}</h1>
                <ul>
                    {this.state.parents.map(parent => this.getParentHtml(parent))}
                </ul>
                <button onClick={() => this.fetchParents()}>fetch</button>
            </div>
        )
    }
}

export default AbfParentList;