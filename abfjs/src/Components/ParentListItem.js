import React from 'react';

/**
 * Shows a single parents (with color/comments)
 */
class ParentListItem extends React.Component {

    static defaultProps = {
        parentInfo: "undefined",
    }

    render() {

        // note: JSON element names depend on PHP API
        const parentID = String(this.props.parentInfo[0]);
        const childCount = this.props.parentInfo[1]["child-count"];
        const color = this.props.parentInfo[1]["color"];
        const comment = this.props.parentInfo[1]["comment"];
        //const group = this.props.parentInfo[1]["group"];

        const comment2 = comment === "?" ? null : comment;

        if (childCount === 0)
            return null;

        return (
            <div key={parentID} style={{ fontFamily: 'monospace', fontSize: '.8em', }}>

                <span style={{ backgroundColor: color }}>
                    {parentID}
                </span>

                <div style={{ display: 'inline-block' }}>
                    &nbsp;({childCount})&nbsp;
                </div>

                <div style={{ display: 'inline-block' }}>
                    {comment2}
                </div>

            </div>
        );
    }
}

export default ParentListItem;