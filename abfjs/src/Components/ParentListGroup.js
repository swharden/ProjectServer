import React from 'react';
import ParentListItem from "./ParentListItem";

/**
 * Shows a list of parents (with color/comments) under a group heading
 */
class ParentListGroup extends React.Component {

    static defaultProps = {
        group: "not set yet",
        allParentInfos: "not set yet",
    }

    render() {
        if (this.props.group == "not set yet")
            return <div>ERROR: group must be defined</div>;

        if (this.props.allParentInfos == "not set yet")
            return <div>ERROR: allParentInfos must be defined</div>;

        const matchingParentInfos = Object.entries(this.props.allParentInfos)
            .filter(x => x[1]["group"] == this.props.group)
            .sort()

        return (
            <>
                <h3 className='mt-4'>{this.props.group ?? "No Group Defined"}</h3>
                {
                    matchingParentInfos.map(x => <ParentListItem key={x[0]} parentInfo={x} />)
                }
            </>
        )
    }
}

export default ParentListGroup;