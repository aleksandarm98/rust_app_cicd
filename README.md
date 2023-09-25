# Rust Web Application

This Rust web application serves as an example of how to implement continuous integration (CI) and continuous delivery (CD) using GitHub Actions and AWS Elastic Beanstalk. This infrastructure enables automatic testing, building, and deploying of the application with every new commit to the GitHub repository.

## Setup

To set up the CI/CD process for this application, follow these steps:

1. Create an AWS account with a properly configured IAM user for accessing AWS Elastic Beanstalk and CodeDeploy services.

2. Fork this repository or create your own Rust web project on GitHub.

3. In your GitHub repository, create a directory named `.github/workflows` and within it, add a YAML file for configuring GitHub Actions. See the example configuration in the [docker-publish.yml](.github/workflows/docker-publish.yml) file in this repository.

4. In AWS Elastic Beanstalk, create an application and environment to host your Rust application. Configure environment settings and platform according to your needs.

5. Set up GitHub Secrets for accessing AWS, including `AWS_ACCESS_KEY_ID` and `AWS_SECRET_ACCESS_KEY`, to enable GitHub Actions to communicate with AWS.

## GitHub Actions for CI/CD

The GitHub Actions configuration for the CI/CD process can be found in the [docker-publish.yml](.github/workflows/docker-publish.yml) file. This file defines steps for code checkout, building the application, and deploying it to AWS Elastic Beanstalk.

## Elastic Beanstalk Configuration

Inside Elastic Beanstalk, you configure the environment for your application, specify the number of instances, scaling changes, and other settings. Elastic Beanstalk automatically detects changes in the application and deploys them.

## Testing the Application

Once the application is deployed on Elastic Beanstalk, you will receive a public IP address or URL to test and access the application. This IP address is dynamic and adaptable, meaning it automatically adjusts to changes in the environment.

