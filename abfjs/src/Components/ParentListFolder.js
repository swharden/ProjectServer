import React from 'react';
import ParentListItem from "./ParentListItem";

/**
 * Shows a list of parents (with color/comments) from a single folder
 */
class ParentListFolder extends React.Component {

    componentDidMount() {
        this.fetchParents();
    }

    state = {
        json: null,
    };

    fetchParents() {
        const folderPath = 'X:/Data/SD/practice/Scott/2022/2022-01-04-AON';
        const url = `http://192.168.1.9/abf-browser/api/v3/cells-folder/?folder=` + folderPath;
        fetch(url)
            .then(response => response.json())
            .then(json => {
                this.setState({
                    json: json,
                });
            })
    }

    render() {
        if (this.state.json == null) {
            return <>Loading...</>;
        } else {
            return Object.entries(this.state.json["parentInfos"]).map(x => <ParentListItem key={x[0]} parentInfo={x} />)
        }
    }
}

export default ParentListFolder;