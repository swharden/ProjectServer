# Developer Notes

## Project Structure
```bash
# Large projects get a folder with notes, data, and analyses.
X:/Data/strain/project/
X:/Data/strain/project/design.ppt
X:/Data/strain/project/notebook.docx
X:/Data/strain/project/micrographs/ihc-20x.tif
X:/Data/strain/project/video/gcamp.avi
X:/Data/strain/project/2p/LineScan-123/frame-001.ch0.tif
X:/Data/strain/project/analysis/figures.opj

# E-phys projects have an 'abfs' folder for notes and data.
X:/Data/strain/project/abfs/
X:/Data/strain/project/abfs/abfProject.txt
X:/Data/strain/project/abfs/2022-12-24-DIC3/abfDay.txt
X:/Data/strain/project/abfs/2022-12-24-DIC3/2022_12_24_0003.abf
X:/Data/strain/project/abfs/2022-12-24-DIC3/2022_12_24_0003.tif
```

* `abfProject.txt` describes things that span days (e.g., experiment design, high-order groups, etc).
* `abfDay.txt` describes things specific to that day (e.g., animal age, individual cell comments).
* The `project` folder can be moved anywhere.
* The `abfs` folder can be moved anywhere.
* _Contents_ of the `abf` folder should not be broken apart.

## ABF Day Notes
```json
{
    "version": "4.0",
    "operator": "scott",
    "animal": "strain:c57 sex:male DOB:2021-01-01",
    "bath": "ACSF +DNQX/AP5",
    "internal": "k-glu (9 mM chloride)",
    "drugs": "3 mL syringe w/ 10 mM TGOT (200 nM bath)",
    "notes": "17 days after surgery",
    "cells": [
        {
            "id": "2022_02_22_DIC3_0000",
            "color": "green",
            "comment": "",
            "group": "Horizontal"
        },
        {
            "id": "2022_02_22_DIC3_0007",
            "color": "red",
            "comment": "died",
            "group": "Horizontal"
        },
        {
            "id": "2022_02_22_DIC3_0020",
            "color": "green",
            "comment": "",
            "group": "Coronal"
        },
        {
            "id": "2022_02_22_DIC3_0057",
            "color": "green",
            "comment": "died",
            "group": "Coronal"
        }
    ]
}
```

## ABF Project Notes
```json
{
    "title": "Important Project",
    "subtitle": "Looking into the things that matter",
    "notes": "project-level hypotheses go here",
    "groups": "information about second-order grouping? Consider PFC vs CA1 PYR and FSIs.",
}
```

* Paths do not need to be defined because all subfolders in this directory are daily folders.
* This is what prevents anything from breaking when folders are moved.
* This is what makes it important to move the `abfs` folder as a whole (and not its subfolders).