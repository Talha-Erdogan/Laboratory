﻿using Laboratory.API.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.API.Business.Interfaces
{
    public interface IProfileDetailService
    {
        List<Auth> GetAllAuthByCurrentUser(int employeeId);
        List<Auth> GetAllAuthByProfileId(int profileId);
        List<Auth> GetAllAuthByProfileIdWhichIsNotIncluded(int profileId);
        string GetAllAuthCodeByEmployeeIdAsConcatenateString(int employeeId);
        int Add(ProfileDetail record);
        int DeleteByProfileIdAndAuthId(int profileId, int authId);
    }
}
