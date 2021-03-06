﻿using System.Collections.Generic;
using AutoMapper;
using MTGAHelper.Entity;
using MTGAHelper.Lib.OutputLogParser;
using MTGAHelper.Tracker.WPF.Models;
using MTGAHelper.Web.Models.Response.User;

namespace MTGAHelper.Tracker.WPF.Business
{
    public class DraftCardsPicker
    {
        private readonly DraftPicksCalculator DraftPicksCalculator;

        public DraftCardsPicker(DraftPicksCalculator draftPicksCalculator)
        {
            DraftPicksCalculator = draftPicksCalculator;
        }

        public ICollection<CardDraftPickWpf> GetDraftPicksForCards(string userId, ICollection<int> grpIds, string source, Dictionary<int, int> collection, ICollection<CardCompareInfo> raredraftingInfo)
        {
            //var apiResponse = api.GetCardsForDraftPick(userId, grpIds, source);

            var result = DraftPicksCalculator.GetCardsForDraftPick(userId, source, grpIds, collection, raredraftingInfo);
            
            var apiDto = Mapper.Map<ICollection<CardForDraftPickDto>>(result);

            var ret = Mapper.Map<ICollection<CardDraftPickWpf>>(apiDto);

            foreach (var c in ret)
                c.ImageArtUrl = new Tools.Utilities().GetThumbnailLocal(c.ImageArtUrl);

            return ret;
        }
    }
}
