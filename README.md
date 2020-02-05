# ATF
Automation Trail Fixer

This project is a simple application with a few test project which all has different issues in the test, making them running in CI.

The goal of the user is to make this project run on CI. With all test passing

## TODO:
Add more strange things in test
Write a manual on how to use it. - kinda done
Write a guide of the bugs and possible solutions

# Instruction:
The pipeline is working, and the test is green. 
But it is a false green!
Your goal is:
figure out what going on
Fix pipeline or code to make the test work.
Bonus:
rewrite test in a way that they will work properly:
write a test for uncovered features

Note the app code is working - but that doesn't mean it doesn't have issues :) (in fact it has a lot of them)

Repo in Github:

Fork this repo https://github.com/mwyrodek/ATF
Login to [Azure DevOps](https://dev.azure.com/)
Create a free public project
go to pipeline
select GitHub 
log in to your GitHub
select your project
Azure should automatically find YAML file in the repo
Click run 
Project is green but in reality, no test where run.
Have fun fixing it

All in AzureDevOps:
Login to [Azure DevOps](https://dev.azure.com/):
Create a free public project.
Go to repos
import repo: https://github.com/mwyrodek/ATF
Go to pipeline
Select azure git as a source.
Azure should automatically find YAML file in repo
Click run 
Project is green but in reality, no test where run.
Have fun fixing it

