#!/bin/bash
export CHROME_BIN=/usr/bin/chromium
if [ ! -d "/home/coder/project/workspace/angularapp" ]
then
    cp -r /home/coder/project/workspace/karma/angularapp /home/coder/project/workspace/;
fi

if [ -d "/home/coder/project/workspace/angularapp" ]
then
    echo "project folder present"
    cp /home/coder/project/workspace/karma/karma.conf.js /home/coder/project/workspace/angularapp/karma.conf.js;

    # checking for admineditloan.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/admineditloan" ]
    then
        cp /home/coder/project/workspace/karma/admineditloan.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/admineditloan/admineditloan.component.spec.ts;
    else
        echo "Frontend_should_create_admineditloan_component FAILED";
        echo "Frontend_should_contain_edit_loan_heading_in_the_admineditloan_component FAILED";
    fi

    # checking for adminnav.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/adminnav" ]
    then
        cp /home/coder/project/workspace/karma/adminnav.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/adminnav/adminnav.component.spec.ts;
    else
        echo "Frontend_should_create_adminnav_component FAILED";
    fi

    # checking for adminviewfeedback.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/adminviewfeedback" ]
    then
        cp /home/coder/project/workspace/karma/adminviewfeedback.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/adminviewfeedback/adminviewfeedback.component.spec.ts;
    else
        echo "Frontend_should_create_adminviewfeedback_component FAILED";
        echo "Frontend_should_contain_feedback_details_heading_in_the_adminviewfeedback_component FAILED";
    fi

    # checking for auth.service.spec.ts component
    if [ -e "/home/coder/project/workspace/angularapp/src/app/services/auth.service.ts" ]
    then
        cp /home/coder/project/workspace/karma/auth.service.spec.ts /home/coder/project/workspace/angularapp/src/app/services/auth.service.spec.ts;
    else
        echo "Frontend_should_create_auth_service FAILED";
    fi

    # checking for createloan.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/createloan" ]
    then
        cp /home/coder/project/workspace/karma/createloan.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/createloan/createloan.component.spec.ts;
    else
        echo "Frontend_should_create_createloan_component FAILED";
        echo "Frontend_should_contain_create_new_loan_heading_in_the_createloan_component FAILED";
    fi

    # checking for error.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/error" ]
    then
        cp /home/coder/project/workspace/karma/error.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/error/error.component.spec.ts;
    else
        echo "Frontend_should_create_error_component FAILED";
        echo "Frontend_should_contain_wrong_message_in_the_error_component FAILED";
    fi

    # checking for feedback.service.spec.ts component
    if [ -e "/home/coder/project/workspace/angularapp/src/app/services/feedback.service.ts" ]
    then
        cp /home/coder/project/workspace/karma/feedback.service.spec.ts /home/coder/project/workspace/angularapp/src/app/services/feedback.service.spec.ts;
    else
        echo "Frontend_should_create_feedback_service FAILED";
    fi

    # checking for home.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/home" ]
    then
        cp /home/coder/project/workspace/karma/home.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/home/home.component.spec.ts;
    else
        echo "Frontend_should_create_home_component FAILED";
        echo "Frontend_should_contain_vehicle_loan_hub_heading_in_the_home_component FAILED";
    fi

    # checking for loan.service.spec.ts component
    if [ -e "/home/coder/project/workspace/angularapp/src/app/services/loan.service.ts" ]
    then
        cp /home/coder/project/workspace/karma/loan.service.spec.ts /home/coder/project/workspace/angularapp/src/app/services/loan.service.spec.ts;
    else
        echo "Frontend_should_create_loan_service FAILED";
    fi

    # checking for loanform.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/loanform" ]
    then
        cp /home/coder/project/workspace/karma/loanform.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/loanform/loanform.component.spec.ts;
    else
        echo "Frontend_should_create_loanform_component FAILED";
        echo "Frontend_should_contain_loan_application_form_heading_in_the_loanform_component FAILED";
    fi

    # checking for login.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/login" ]
    then
        cp /home/coder/project/workspace/karma/login.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/login/login.component.spec.ts;
    else
        echo "Frontend_should_create_login_component FAILED";
        echo "Frontend_should_contain_login_heading_in_the_login_component FAILED";
    fi

    # checking for navbar.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/navbar" ]
    then
        cp /home/coder/project/workspace/karma/navbar.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/navbar/navbar.component.spec.ts;
    else
        echo "Frontend_should_create_navbar_component FAILED";
        echo "Frontend_should_contain_vehicle_loan_hub_heading_in_the_navbar_component FAILED";
    fi

    # checking for registration.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/registration" ]
    then
        cp /home/coder/project/workspace/karma/registration.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/registration/registration.component.spec.ts;
    else
        echo "Frontend_should_create_registration_component FAILED";
        echo "Frontend_should_contain_registration_heading_in_the_registration_component FAILED";
    fi

    # checking for requestedloan.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/requestedloan" ]
    then
        cp /home/coder/project/workspace/karma/requestedloan.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/requestedloan/requestedloan.component.spec.ts;
    else
        echo "Frontend_should_create_requestedloan_component FAILED";
        echo "Frontend_should_contain_loan_requests_for_approval_heading_in_the_requestedloan_component FAILED";
    fi

    # checking for useraddfeedback.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/useraddfeedback" ]
    then
        cp /home/coder/project/workspace/karma/useraddfeedback.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/useraddfeedback/useraddfeedback.component.spec.ts;
    else
        echo "Frontend_should_create_useraddfeedback_component FAILED";
        echo "Frontend_should_contain_add_feedback_heading_in_the_useraddfeedback_component FAILED";
    fi

    # checking for userappliedloan.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/userappliedloan" ]
    then
        cp /home/coder/project/workspace/karma/userappliedloan.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/userappliedloan/userappliedloan.component.spec.ts;
    else
        echo "Frontend_should_create_userappliedloan_component FAILED";
        echo "Frontend_should_contain_applied_loans_heading_in_the_userappliedloan_component FAILED";
    fi

    # checking for usernav.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/usernav" ]
    then
        cp /home/coder/project/workspace/karma/usernav.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/usernav/usernav.component.spec.ts;
    else
        echo "Frontend_should_create_usernav_component FAILED";
    fi

    # checking for userviewfeedback.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/userviewfeedback" ]
    then
        cp /home/coder/project/workspace/karma/userviewfeedback.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/userviewfeedback/userviewfeedback.component.spec.ts;
    else
        echo "Frontend_should_create_userviewfeedback_component FAILED";
        echo "Frontend_should_contain_my_feedback_heading_in_the_userviewfeedback_component FAILED";
    fi

    # checking for userviewloan.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/userviewloan" ]
    then
        cp /home/coder/project/workspace/karma/userviewloan.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/userviewloan/userviewloan.component.spec.ts;
    else
        echo "Frontend_should_create_userviewloan_component FAILED";
        echo "Frontend_should_contain_available_vehicle_loans_heading_in_the_userviewloan_component FAILED";
    fi

    # checking for viewloan.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/viewloan" ]
    then
        cp /home/coder/project/workspace/karma/viewloan.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/viewloan/viewloan.component.spec.ts;
    else
        echo "Frontend_should_create_viewloan_component FAILED";
        echo "Frontend_should_contain_vehicle_loans_heading_in_the_viewloan_component FAILED";
    fi

    if [ -d "/home/coder/project/workspace/angularapp/node_modules" ]; 
    then
        cd /home/coder/project/workspace/angularapp/
        npm test;
    else
        cd /home/coder/project/workspace/angularapp/
        yes | npm install
        npm test
    fi 
else   
    echo "Frontend_should_create_admineditloan_component FAILED";
    echo "Frontend_should_contain_edit_loan_heading_in_the_admineditloan_component FAILED";
    echo "Frontend_should_create_adminnav_component FAILED";
    echo "Frontend_should_create_adminviewfeedback_component FAILED";
    echo "Frontend_should_contain_feedback_details_heading_in_the_adminviewfeedback_component FAILED";
    echo "Frontend_should_create_auth_service FAILED";
    echo "Frontend_should_create_createloan_component FAILED";
    echo "Frontend_should_contain_create_new_loan_heading_in_the_createloan_component FAILED";
    echo "Frontend_should_create_error_component FAILED";
    echo "Frontend_should_contain_wrong_message_in_the_error_component FAILED";
    echo "Frontend_should_create_feedback_service FAILED";
    echo "Frontend_should_create_home_component FAILED";
    echo "Frontend_should_contain_vehicle_loan_hub_heading_in_the_home_component FAILED";
    echo "Frontend_should_create_loan_service FAILED";
    echo "Frontend_should_create_loanform_component FAILED";
    echo "Frontend_should_contain_loan_application_form_heading_in_the_loanform_component FAILED";
    echo "Frontend_should_create_login_component FAILED";
    echo "Frontend_should_contain_login_heading_in_the_login_component FAILED";
    echo "Frontend_should_create_navbar_component FAILED";
    echo "Frontend_should_contain_vehicle_loan_hub_heading_in_the_navbar_component FAILED";
    echo "Frontend_should_create_registration_component FAILED";
    echo "Frontend_should_contain_registration_heading_in_the_registration_component FAILED";
    echo "Frontend_should_create_requestedloan_component FAILED";
    echo "Frontend_should_contain_loan_requests_for_approval_heading_in_the_requestedloan_component FAILED";
    echo "Frontend_should_create_useraddfeedback_component FAILED";
    echo "Frontend_should_contain_add_feedback_heading_in_the_useraddfeedback_component FAILED";
    echo "Frontend_should_create_userappliedloan_component FAILED";
    echo "Frontend_should_contain_applied_loans_heading_in_the_userappliedloan_component FAILED";
    echo "Frontend_should_create_usernav_component FAILED";
    echo "Frontend_should_create_userviewfeedback_component FAILED";
    echo "Frontend_should_contain_my_feedback_heading_in_the_userviewfeedback_component FAILED";
    echo "Frontend_should_create_userviewloan_component FAILED";
    echo "Frontend_should_contain_available_vehicle_loans_heading_in_the_userviewloan_component FAILED";
    echo "Frontend_should_create_viewloan_component FAILED";
    echo "Frontend_should_contain_vehicle_loans_heading_in_the_viewloan_component FAILED";
fi
