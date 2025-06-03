# This is the Readme for the b-lister 

# Collaborators
- Brandon Crespo
- Yoga Gunawan
- Thomas Sornig
- Jacob Zander Gino
- Ethan (Bambi) Davis

# git commands

`git branch`
- Gives you a list of all the branches,
- Indicates the current branch,

`git add -A`
- Adds all changes to be staged,
- A = All,

`git status`
- Shows the status of  all the files
- Red = not up-to-date with the stage,
- Green = up-to-date with the stage,

`git commit -m 'message'`
- commits the staged changes to the current branch with a message,

`git push origin branchName`
- pushes the commit changes to the specified repository and branch,

`git checkout nameOfBranch`
- switches to the specified branch,

`git checkout -b nameOfBranch`
- creates and switches to a new branch named nameOfBranch,

`git merge nameOfBranch`
- merges the specified branch into the current branch,

`git tag -a v1.0.0 -m 'versionName 1.0.0'`
- tags the most recent pushed commit on the current branch with the tag v1.0.0,

`git push origin tagName`
- pushes the specified tag to origin