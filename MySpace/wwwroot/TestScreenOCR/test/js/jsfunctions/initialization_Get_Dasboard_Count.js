function initialization_Get_Dasboard_Count() {

    $.ajax({
        type: "GET",
        url: "/Home/Get_Dashboard_Count",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var data = JSON.parse(response);
            if (data[0].UserType == 0 || data[0].UserType == 9) {

                document.getElementById("usr_crfcount").innerText = data[0].Total;
                document.getElementById("usr_ongngcount").innerText = data[0].Ongoing;
                document.getElementById("usr_usraccptncecount").innerText = data[0].UserAcceptance;
                document.getElementById("usr_feedbackcount").innerText = data[0].UserFeedback;
                document.getElementById("usr_delayedcount").innerText = data[0].Delayed;
                document.getElementById("usr_lveclsecount").innerText = data[0].LiveAndClosed;
                document.getElementById("usr_rejectedcount").innerText = data[0].Rejected;

            }
           
           
            else if (data[0].UserType == 1) {

                document.getElementById("Hod_crfcount").innerText = data[0].Total;
                document.getElementById("Hod_pndingcount").innerText = data[0].Recommendation_pndg;
                document.getElementById("Hod_ongngcount").innerText = data[0].Ongoing;
                document.getElementById("Hod_delayedcount").innerText = data[0].Delay;
                document.getElementById("Hod_lveclsecount").innerText = data[0].LiveAndClosed;
                document.getElementById("Hod_rejectedcount").innerText = data[0].Rejected;

            }

            else if (data[0].UserType == 2) {

                document.getElementById("Mash_Ithead_crfcount").innerText = data[0].Total_MASH;
                document.getElementById("Mash_Ithead_pndingcount").innerText = data[0].Recommendation_pndg_MASH;
                document.getElementById("Mash_Ithead_ongngcount").innerText = data[0].Ongoing_MASH;
                document.getElementById("Mash_Ithead_delyedcount").innerText = data[0].Delay_MASH;
                document.getElementById("Mash_Ithead_lveclosecount").innerText = data[0].LiveAndClosed_MASH;
                document.getElementById("Mash_Ithead_usracceptncecount").innerText = data[0].UserAcceptance_MASH;
                document.getElementById("Mash_Ithead_usrfeedbckcount").innerText = data[0].UserFeedback_MASH;
                document.getElementById("Mash_ithead_rejectedcount").innerText = data[0].Rejected_MASH;
                document.getElementById("Mash_Itcoordinator_prev_month_live_closed").innerText = data[0].TL_live_close_prev_month_MASH;
                document.getElementById("Mash_Itcoordinator_total_live_closed_this_month").innerText = data[0].Curr_Mnth_TL_live_close_MASH;

                document.getElementById("Mafound_Ithead_crfcount").innerText = data[0].Total;
                document.getElementById("Mafound_Ithead_pndingcount").innerText = data[0].Recommendation_pndg;
                document.getElementById("Mafound_Ithead_ongngcount").innerText = data[0].Ongoing;
                document.getElementById("Mafound_Ithead_delyedcount").innerText = data[0].Delay;
                document.getElementById("Mafound_Ithead_lveclosecount").innerText = data[0].LiveAndClosed;
                document.getElementById("Mafound_Ithead_usracceptncecount").innerText = data[0].UserAcceptance;
                document.getElementById("Mafound_Ithead_usrfeedbckcount").innerText = data[0].UserFeedback;
                document.getElementById("Mafound_ithead_rejectedcount").innerText = data[0].Rejected;
                document.getElementById("Mafound_Itcoordinator_prev_month_tl_live_closed").innerText = data[0].TL_live_close_prev_month;
                document.getElementById("Mafound_Itcoordinator_total_live_closed_this_month").innerText = data[0].Curr_Mnth_TL_live_close;

            }

            else if (data[0].UserType == 3) {

                document.getElementById("Mash_head_crfcount").innerText = data[0].Total_MASH;
                document.getElementById("Mash_head_pndingcount").innerText = data[0].Recommendation_pndg_MASH;
                document.getElementById("Mash_head_ongngcount").innerText = data[0].Ongoing_MASH;
                document.getElementById("Mash_head_delayedcount").innerText = data[0].Delay_MASH;
                document.getElementById("Mash_head_lveclosecount").innerText = data[0].LiveAndClosed_MASH;
                document.getElementById("Mash_head_rejectedcount").innerText = data[0].Rejected_MASH;

                document.getElementById("Mafound_head_crfcount").innerText = data[0].Total;
                document.getElementById("Mafound_head_pndingcount").innerText = data[0].Recommendation_pndg;
                document.getElementById("Mafound_head_ongngcount").innerText = data[0].Ongoing;
                document.getElementById("Mafound_head_delayedcount").innerText = data[0].Delay;
                document.getElementById("Mafound_head_lveclosecount").innerText = data[0].LiveAndClosed;
                document.getElementById("Mafound_head_rejectedcount").innerText = data[0].Rejected;

            }
            else if (data[0].UserType == 4 ) {
                
                document.getElementById("techlead_crfcount").innerText = data[0].Total;
                document.getElementById("techlead_ongngcount").innerText = data[0].Ongoing;
                document.getElementById("techlead_delycount").innerText = data[0].Delay;
                document.getElementById("techlead_lveandclosecount").innerText = data[0].LiveAndClosed;
                document.getElementById("techlead_tapndingcount").innerText = data[0].TA_pndg;
                document.getElementById("techlead_rejected_del_count").innerText = data[0].Rejected;
                document.getElementById("teachlead_hod_rec_pndg").innerText = data[0].HOD_pending;
                document.getElementById("techlead_it_head_rec_pndg").innerText = data[0].IT_HEAD_pending;
                document.getElementById("head_aprl_pndg_techlead").innerText = data[0].HEAD_Pending;
                document.getElementById("prev_month_tl_live_closed").innerText = data[0].TL_live_close_prev_month;
                document.getElementById("teachlead_total_live_closed_this_month").innerText = data[0].Curr_Mnth_TL_live_close;
                document.getElementById("techlead_rejectedcount").innerText = data[0].HOD_Rejected_Techlead;



            }
            else if (data[0].UserType == 5) {

                document.getElementById("developer_crfcount").innerText = data[0].Total;
                document.getElementById("developer_ongngcount").innerText = data[0].Ongoing;
                document.getElementById("developer_dlyedcount").innerText = data[0].Delay;
                document.getElementById("developer_lveandclosecount").innerText = data[0].LiveAndClosed;
                document.getElementById("developer_usrfeedbckcount").innerText = data[0].UserFeedback;
                document.getElementById("developer_bugcount").innerText = data[0].Bug_count;

            }
            else if (data[0].UserType == 6) {

                document.getElementById("testlead_crfcount").innerText = data[0].Total;
                document.getElementById("testlead_ongngcount").innerText = data[0].Ongoing;
                document.getElementById("testlead_dlyedcount").innerText = data[0].Delayed;
                document.getElementById("testlead_lveandclosecount").innerText = data[0].LiveAndClosed;
                document.getElementById("testlead_tapnding").innerText = data[0].Recommendation_pndg;

            }
            else if (data[0].UserType == 7) {

                document.getElementById("tester_crfcount").innerText = data[0].Total;
                document.getElementById("tester_ongngcount").innerText = data[0].Ongoing;
                document.getElementById("tester_dlyedcount").innerText = data[0].Delayed;
                document.getElementById("tester_lveclosecount").innerText = data[0].LiveAndClosed;
                document.getElementById("tester_bugcount").innerText = data[0].Bugreport_Tester;

            }
            else if (data[0].UserType == 8 || data[0].UserType == 10) {

                document.getElementById("Mash_Itcoordinator_crfcount").innerText = data[0].Total_MASH;
                document.getElementById("Mash_Itcoordinator_ongngcount").innerText = data[0].Ongoing_MASH;
                document.getElementById("Mash_Itcoordinator_delyedcount").innerText = data[0].Delay_MASH;
                document.getElementById("Mash_Itcoordinator_lveclosecount").innerText = data[0].LiveAndClosed_MASH;
                document.getElementById("Mash_Itcoordinator_rejectedcount").innerText = data[0].Rejected_MASH;
                document.getElementById("Mash_Itcoordinator_HOD_Rec_Pndg").innerText = data[0].HOD_pending_MASH;
                document.getElementById("Mash_Itcoordinator_IT_Head_Rec_Pndg").innerText = data[0].IT_HEAD_pending_MASH;
                document.getElementById("Mash_Itcoordinator_Head_Aprl_Pndg").innerText = data[0].HEAD_Pending_MASH;
                document.getElementById("Mash_Itcoordinator_prev_month_live_closed").innerText = data[0].TL_live_close_prev_month_MASH;
                document.getElementById("Mash_Itcoordinator_total_live_closed_this_month").innerText = data[0].Curr_Mnth_TL_live_close_MASH;


                document.getElementById("Mafound_Itcoordinator_crfcount").innerText = data[0].Total;
                document.getElementById("Mafound_Itcoordinator_ongngcount").innerText = data[0].Ongoing;
                document.getElementById("Mafound_Itcoordinator_delyedcount").innerText = data[0].Delay;
                document.getElementById("Mafound_Itcoordinator_lveclosecount").innerText = data[0].LiveAndClosed;
                document.getElementById("Mafound_Itcoordinator_rejectedcount").innerText = data[0].Rejected;
                document.getElementById("Mafound_Itcoordinator_HOD_Rec_Pndg").innerText = data[0].HOD_pending;
                document.getElementById("Mafound_Itcoordinator_IT_Head_Rec_Pndg").innerText = data[0].IT_HEAD_pending;
                document.getElementById("Mafound_Itcoordinator_Head_Aprl_Pndg").innerText = data[0].HEAD_Pending;
                document.getElementById("Mafound_Itcoordinator_prev_month_tl_live_closed").innerText = data[0].TL_live_close_prev_month;
                document.getElementById("Mafound_Itcoordinator_total_live_closed_this_month").innerText = data[0].Curr_Mnth_TL_live_close;

            }
        }

    });

}