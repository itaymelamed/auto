pipeline {
    agent any
    stages {
        stage('Test') {
            steps {
                echo 'test'
            }
        }
    }
    post {
        always {
            junit '/var/jenkins_home/workspace/Nightly/TestResult.xml'
        }
    }
}
