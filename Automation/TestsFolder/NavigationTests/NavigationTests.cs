using Automation.PagesObjects;
using NUnit.Framework;

namespace Automation.TestsFolder.NavigateionTests
{
    public class NavigationTests
    {
        [TestFixture]
        [Parallelizable]
        public class Fixtures : BaseUi
        {
            [Test]
            [Property("TestCaseId", "99")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Category("90Min")]
            [Category("Ftb90")]
            [Retry(3)]
            public void FixturesIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Transfers : BaseUi
        {
            [Test]
            [Property("TestCaseId", "105")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Category("90Min")]
            [Category("Ftb90")]
            [Retry(3)]
            public void TransfersIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Buzz : BaseUi
        {
            [Test]
            [Property("TestCaseId", "107")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Category("90Min")]
            [Category("Ftb90")]
            [Category("12Up")]
            [Retry(3)]
            public void BuzzIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class UEFA : BaseUi
        {
            [Test]
            [Property("TestCaseId", "108")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Category("90Min")]
            [Category("Ftb9090")]
            [Retry(3)]
            public void UEFAIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class WorldNews : BaseUi
        {
            [Test]
            [Property("TestCaseId", "109")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Category("90Min")]
            [Category("Ftb90")]
            [Retry(3)]
            public void WorldNewsIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Lists : BaseUi
        {
            [Test]
            [Property("TestCaseId", "110")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Category("90Min")]
            [Category("Ftb90")]
            [Category("12Up")]
            [Category("Floor8")]
            [Retry(3)]
            public void ListsIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Viral : BaseUi
        {
            [Test]
            [Property("TestCaseId", "111")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Category("90Min")]
            [Category("Floor8")]
            [Retry(3)]
            public void ViralIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Conmebol : BaseUi
        {
            [Test]
            [Property("TestCaseId", "112")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90Min")]
            [Retry(3)]
            public void ConmebolIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Rumors : BaseUi
        {
            [Test]
            [Property("TestCaseId", "113")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90Min")]
            [Retry(3)]
            public void RumorsIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }
    
        [TestFixture]
        [Parallelizable]
        public class LocalCoverage : BaseUi
        {
            [Test]
            [Property("TestCaseId", "114")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90Min")]
            [Category("Ftb90")]
            [Retry(3)]
            public void LocalCoverageIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Esports : BaseUi
        {
            [Test]
            [Property("TestCaseId", "115")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90Min")]
            [Retry(3)]
            public void EsportsIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }


        [TestFixture]
        [Parallelizable]
        public class Highlights : BaseUi
        {
            [Test]
            [Property("TestCaseId", "116")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("12Up")]
            [Retry(3)]
            public void HighlightsIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Quizzes : BaseUi
        {
            [Test]
            [Property("TestCaseId", "117")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("12Up")]
            [Category("Floor8")]
            [Retry(3)]
            public void QuizzesIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Tv : BaseUi
        {
            [Test]
            [Property("TestCaseId", "118")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("Floor8")]
            [Retry(3)]
            public void TvIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Music : BaseUi
        {
            [Test]
            [Property("TestCaseId", "119")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("Floor8")]
            [Retry(3)]
            public void MusicIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Gossip : BaseUi
        {
            [Test]
            [Property("TestCaseId", "120")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("Floor8")]
            [Retry(3)]
            public void GossipIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Video : BaseUi
        {
            [Test]
            [Property("TestCaseId", "121")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("DBLTAP")]
            [Retry(3)]
            public void VideoIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class ESL : BaseUi
        {
            [Test]
            [Property("TestCaseId", "122")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("DBLTAP")]
            [Retry(3)]
            public void ESLIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class DreamHack : BaseUi
        {
            [Test]
            [Property("TestCaseId", "123")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("DBLTAP")]
            [Retry(3)]
            public void DreamHackIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class MDL : BaseUi
        {
            [Test]
            [Property("TestCaseId", "124")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("DBLTAP")]
            [Retry(3)]
            public void MDLIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Fnatic : BaseUi
        {
            [Test]
            [Property("TestCaseId", "125")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("DBLTAP")]
            [Retry(3)]
            public void FnaticIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class iLeague : BaseUi
        {
            [Test]
            [Property("TestCaseId", "126")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("90MinIn")]
            [Retry(3)]
            public void iLeagueIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Latest : BaseUi
        {
            [Test]
            [Property("TestCaseId", "129")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("Pluralist")]
            [Retry(3)]
            public void LatestIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Politics : BaseUi
        {
            [Test]
            [Property("TestCaseId", "130")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("Politics")]
            [Retry(3)]
            public void PoliticsIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Perspectives : BaseUi
        {
            [Test]
            [Property("TestCaseId", "131")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("Pluralist")]
            [Retry(3)]
            public void PerspectivesIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Media : BaseUi
        {
            [Test]
            [Property("TestCaseId", "132")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("Pluralist")]
            [Retry(3)]
            public void MediaIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }

        [TestFixture]
        [Parallelizable]
        public class Culture : BaseUi
        {
            [Test]
            [Property("TestCaseId", "133")]
            [Category("Sanity")]
            [Category("Header")]
            [Category("Navigation")]
            [Category("Pluralist")]
            [Retry(3)]
            public void CultureIcon()
            {
                Navigation navigation = new Navigation(_browser);
                string errors = navigation.ValidateIcon(_params);

                Assert.True(string.IsNullOrEmpty(errors), errors);
            }
        }
    }
}