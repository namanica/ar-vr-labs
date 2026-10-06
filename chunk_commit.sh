#!/bin/bash
git add .gitattributes
git commit -m "build: configure git lfs for unity 3d assets"
git rm -r --cached .
git add .gitattributes
git commit --amend --no-edit

git add Assets/DuNguyn
git commit -m "feat: add DuNguyn gramophone assets"

git add "Assets/Furniture Mega Pack"
git commit -m "feat: add Furniture Mega Pack assets"

git add Assets/QA_Books
git commit -m "feat: add QA_Books assets"

git add Assets/TV*
git commit -m "feat: add TV LED 30 asset"

git add Assets/Scenes
git commit -m "feat: setup Lab2 VR scene"

git add Assets/Settings Assets/DefaultVolumeProfile.* ProjectSettings
git commit -m "chore: setup universal render pipeline settings"

git add .
git commit -m "chore: add remaining meta files"
