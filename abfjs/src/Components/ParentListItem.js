import React from 'react';

/**
 * Shows a single parents (with color/comments)
 */
class ParentListItem extends React.Component {

    onAbfClicked() {
        // TODO: use parentCallback
        console.log("CLICKED: " + this.props.cell["parentID"]);
    }
    
    render() {

        if (this.props.cell == null)
            return <div>no cell</div>

        // TODO: ew this is yuck
        const parentID = this.props.cell["parentID"];
        const childCount = this.props.cell["abfPaths"].length;
        const color = this.props.cell["color"];
        const comment = this.props.cell["comment"];
        const comment2 = comment === "?" ? null : comment;

        if (childCount === 0)
            return null;

        return (
            <div key={parentID} style={{ fontFamily: 'monospace', fontSize: '.8em', }}>

                <span className='px-1' style={{ backgroundColor: color }}>
                    <a className='text-dark text-decoration-none' onClick={() => this.onAbfClicked()}>{parentID}</a>
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