import React from 'react';
import ParentListItem from "./ParentListItem";

/**
 * Shows a list of parents (with color/comments) under a group heading
 */
class ParentListGroup extends React.Component {

    static defaultProps = {
        group: "not set yet",
        cells: "not set yet",
    }

    render() {
        if (this.props.group === "not set yet")
            return <div>ERROR: group must be defined</div>;

        if (this.props.cells === "not set yet")
            return <div>ERROR: cells must be defined</div>;

        const matchingCells = Object.entries(this.props.cells)
            .filter(x => x[1]["group"] === this.props.group)
            .sort()

        return (
            <>
                <h3 className='mt-4'>{this.props.group ?? "No Group Defined"}</h3>
                {
                    matchingCells.map(([k, v]) => <ParentListItem key={k} cell={v} />)
                }
            </>
        )
    }
}

export default ParentListGroup;