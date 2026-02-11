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
                sh 'dotnet restore'
                sh 'dotnet build'
                sh 'pwsh bin/Debug/net8.0/playwright.ps1 install'
            }
        }
        stage('Test') {
            steps {
                echo 'Running Playwright Tests...'
                sh 'dotnet test --logger "trx;LogFileName=test_results.trx"'
            }
        }
    } // <--- The "stages" block ends here.
    
    // The "post" block must be AFTER the closing bracket of "stages"
    post {
        always {
            mstest testResultsFile: '**/*.trx', keepLongStdio: true
        }
    }
}
