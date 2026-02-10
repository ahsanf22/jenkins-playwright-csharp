pipeline {
    agent {
        docker { 
            // We use the image we already tested successfully!
            image 'mcr.microsoft.com/playwright/dotnet:v1.41.0-jammy' 
            reuseNode true
        }
    }
    stages {
        stage('Install Deps') {
            steps {
                sh 'dotnet restore'
                sh 'pwsh bin/Debug/net8.0/playwright.ps1 install'
            }
        }
        stage('Test') {
            steps {
                // Run tests and produce a result file for Jenkins to read
                sh 'dotnet test --logger "trx;LogFileName=test_results.trx"'
            }
        }
    }
}
