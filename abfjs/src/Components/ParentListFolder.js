import React from 'react';
import ParentListGroup from "./ParentListGroup";

/**
 * Shows all groups of parents from a single folder
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
            .then(json => this.setState({ json: json }));
    }

    render() {
        if (this.state.json == null)
            return <>Loading...</>;

        const groups = Object.entries(this.state.json["parentInfos"]).map(([k, v]) => v["group"] ?? "");
        const uniqueGroups = [...new Set(groups)].sort();

        return uniqueGroups.map(group => <ParentListGroup key={group} group={group} json={this.state.json} />);
    }
}

export default ParentListFolder;