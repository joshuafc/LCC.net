﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LandscapeClassifier.ViewModel.MainWindow.Classification;

namespace LandscapeClassifier.Model.Classification
{

    public class ClassificationModel
    {
        public List<ClassifiedFeatureVector> FeatureVectors { get; }
        public List<string> LandCoverTypes { get; set; }
        public List<string> Bands { get; }
        public string Projection { get; }

        public ClassificationModel(string projectionName, List<string> landCoverTypes, List<LayerViewModel> layers, List<ClassifiedFeatureVectorViewModel> allFeaturesView, ImmutableList<LandcoverTypeViewModel> landcoverTypes)
        {
            var indices = layers.Where(b => b.UseFeature).Select((b, i) => i);
            var features = allFeaturesView.Select(f =>
            {
                var featureVector = new FeatureVector(indices.Select(i => f.ClassifiedFeatureVector.FeatureVector.BandIntensities[i]).ToArray());
                return new ClassifiedFeatureVector(landcoverTypes.FindIndex(t => t.Id == f.FeatureTypeViewModel.Id), featureVector, f.Position);
            }).ToList();

        Bands = layers.Where(b => b.UseFeature).Select(b => b.Name).ToList();
            FeatureVectors = features;
            Projection = projectionName;
            LandCoverTypes = landCoverTypes;

        }

    }
}
