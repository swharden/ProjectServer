import React from 'react';

class AbfList extends React.Component {

    constructor(props) {
        super(props);

        // Set initial state 
        this.state = { abfsByParent: {} }
        this.updateState = this.updateState.bind(this)
    }


    getAbfs() {
        console.log("DOING IT");

        const url = `http://192.168.1.9/abf-browser/api/v3/abf-list/?folder=X:/Data/SD/practice/Scott/2022/2022-01-04-AON`;

        fetch(url, { 'cache': 'no-store', 'Cache-Control': 'no-cache' })
            .then(response => response.json())
            .then(obj => {
                console.log(obj);
                const parents = Object.keys(obj["abfs-by-parent"]);
            });
    }

    render() {
        return (
            <div>
                <button onClick={this.getAbfs()}>request</button>
            </div>
        );
    }
}

export default AbfList;