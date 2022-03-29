import React from 'react';

class AbfList extends React.Component {

    state = {
        abfsByParent: null
    };

    getAbfs = () => {
        const url = `http://192.168.1.9/abf-browser/api/v3/abf-list/?folder=X:/Data/SD/practice/Scott/2022/2022-01-04-AON`;

        fetch(url, { 'cache': 'no-store', 'Cache-Control': 'no-cache' })
            .then(response => response.json())
            .then(obj => {
                this.setState({ abfsByParent: obj["abfs-by-parent"] });
            });
    }

    showGroup(parent, abfs) {
        return (
            <section key={parent}>
                <h1 className="mt-3">{parent}</h1>
                {abfs.map((abf) => {
                    return (<div key={abf}>{abf}</div>)
                })}
            </section>
        )
    }

    showAllGroups() {
        if (!this.state.abfsByParent)
            return null;

        return Object.keys(this.state.abfsByParent).map((parent) => {
            let abfs = this.state.abfsByParent[parent];

            if (!abfs.length)
                return null;

            return this.showGroup(parent, abfs);
        });
    }

    render() {
        return (
            <div>
                <button onClick={this.getAbfs}>get ABFs</button>
                {this.showAllGroups()}
            </div>
        );
    }
}

export default AbfList;