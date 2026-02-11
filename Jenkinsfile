pipeline {
    agent {
        docker { 
            image 'mcr.microsoft.com/playwright/dotnet:v1.41.0-jammy' 
            reuseNode true
        }
    }
    stages {
        stage('Build & Install') {
            steps {
                // 1. Restore packages
                sh 'dotnet restore'
                
                // 2. BUILD the project (This creates the bin/ folder!)
                sh 'dotnet build'
                
                // 3. Now the script exists, so we can run itA
                sh 'pwsh bin/Debug/net8.0/playwright.ps1 install'
            }
        }
        stage('Test') {
            steps {
                echo 'Running Playwright Tests...'
                sh 'dotnet test --logger "trx;LogFileName=test_results.trx"'
            }
        }
        stages {
        // ... (your existing stages) ...
    }
    
    // ADD THIS SECTION:
    post {
        always {
            // This tells Jenkins to look for the TRX file we generated
            mstest testResultsFile: '**/*.trx', keepLongStdio: true
            }
    }
}
