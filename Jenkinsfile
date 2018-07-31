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
            nunit 'TestReults.xml'
        }
    }
}
