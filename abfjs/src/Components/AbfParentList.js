import React from 'react';
import AbfParent from '../Models/AbfParent';

class AbfParentList extends React.Component {

    state = {
        parents: null
    };

    showParent(parent) {
        return <div key={parent.path}>
            <span style={{ backgroundColor: parent.color, paddingLeft: 3, paddingRight: 3 }}>
                {parent.abfID}
            </span>
            <span style={{ paddingLeft: 5 }}>
                {parent.comment}
            </span>
        </div >
    }

    render() {

        let parents = [];
        parents.push(new AbfParent("x:/folder/path1.abf"));
        parents.push(new AbfParent("x:/folder/path2.abf"));
        parents.push(new AbfParent("x:/folder/path3.abf"));

        return parents.map((parent) => this.showParent(parent));
    }
}

export default AbfParentList;