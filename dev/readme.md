# Developer Notes

## Project Structure

* Large projects have a folder with notes, data, and analyses.
* Jason should be involved in naming new projects.
* Project folders can be moved as needed.

```bash
X:/Project/MyProject/
X:/Project/MyProject/design.ppt
X:/Project/MyProject/notebook.docx
X:/Project/MyProject/micrographs/ihc-20x.tif
X:/Project/MyProject/video/gcamp.avi
X:/Project/MyProject/2p/LineScan-123/frame-001.ch0.tif
X:/Project/MyProject/analysis/figures.opj
X:/Project/MyProject/abfs/
```

## ABF Folders

* E-phys projects have an `abfs/` folder containing one or more `experiment` folders
* ABFs are recorded in daily folders inside `experiment` folders

```
/abfs/experiment/date/filename.abf
```

## Notes Files

* Separate notes files exist at the `project`, `experiment`, and `day` level

```
X:/Project/MyProject/project.json
X:/Project/MyProject/abfs/experiment/experiment.json
X:/Project/MyProject/abfs/experiment/date/abf-day.json
```

### Project Notes
```json
{
    "title": "Important Project",
    "description": "Looking into the things that matter",
    "notes": "the plan is to do the thing until the result happens",
    "experimentPaths": [
        "./",
        "X:/Data/some/other/path1/",
        "X:/Data/some/other/path2/"
    ]
}
```

### Experiment Notes
```json
{
	"title": "DrugA Ih",
	"description": "Test how DrugA changes holding current",
	"notes": "run all experiments at -70 in blockers continuously"
}
```

### ABF Day Notes
```json
{
    "operator": "scott",
    "subject": "strain:c57 sex:male DOB:2021-01-01",
    "bath": "ACSF +DNQX/AP5",
    "internal": "k-glu (9 mM chloride)",
    "drugs": "3 mL syringe w/ 10 mM TGOT (200 nM bath)",
    "notes": "17 days after ChR2 injection",
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