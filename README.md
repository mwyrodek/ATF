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

1. Fork this repo https://github.com/mwyrodek/ATF
2. Login to [Azure DevOps](https://dev.azure.com/)
3. Create a free public project
4. go to pipeline
4. select GitHub 
5.Log in to your GitHub
6. select your project
7. Azure should automatically find YAML file in the repo
8. Click run 
9. Project is green but in reality, no test where run.
10. Have fun fixing it :)

All in AzureDevOps:
1. Login to [Azure DevOps](https://dev.azure.com/):
2. Create a free public project.
3. Go to repos
4. import repo: https://github.com/mwyrodek/ATF
5. Go to pipeline
6. Select azure git as a source.
7. Azure should automatically find YAML file in repo
8. Click run 
9. Project is green but in reality, no test where run.
10. Have fun fixing it :)

