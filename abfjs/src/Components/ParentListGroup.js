import React from 'react';
import ParentListItem from "./ParentListItem";

/**
 * Shows a list of parents (with color/comments) grouped under a heading
 */
class ParentListGroup extends React.Component {

    static defaultProps = {
        group: "no group",
        json: null,
    }

    render() {
        if (this.props.json == null)
            return null;

        const matchingParentInfos = Object.entries(this.props.json["parentInfos"])
            .filter(x => x[1]["group"] == this.props.group)
            .sort()

        if (matchingParentInfos.length == 0)
            return null;

        return (
            <>
                <h3>{this.props.group}</h3>
                {
                    matchingParentInfos.map(x => <ParentListItem key={x[0]} parentInfo={x} />)
                }
            </>
        )
    }
}

export default ParentListGroup;